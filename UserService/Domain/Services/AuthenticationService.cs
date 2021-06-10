using Domain.Exceptions;
using Domain.Infrastructure;
using Domain.Models;
using Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserRegistry _UserRepository { get; }
        private ITokenService _TokenService { get; }
        private IAccountsManger _AccountsManager { get; }
        private IMailSender _MailSender { get; }
        private IAccountService _AccountService { get; }

        public AuthenticationService(
            IUserRegistry userRepository, IMailSender mailSender, ITokenService tokenService,
            IAccountsManger accountsManager, IAccountService accountService)
        {
            _UserRepository = userRepository;
            _MailSender = mailSender;
            _TokenService = tokenService;
            _AccountsManager = accountsManager;
            _AccountService = accountService;
        }

        public Task<User> GetIdentity(string email)
        {
            return _UserRepository.GetAsync(email);
        }

        public async Task<string> SignInAsync(SignIn signIn)
        {
            var authenticated = await _UserRepository
                .AuthenticateAsync(signIn.Email, signIn.Password);

            if (authenticated)
            {
                var user = await _UserRepository.GetAsync(signIn.Email);
                if (!user.AccountConfirmed)
                {
                    throw new AuthenticationException("Account is not confirmed.", ExceptionCause.IncorrectInputData);
                }

                return _TokenService.GenerateSecurityToken(signIn.Email);
            }
            else
            {
                throw new AuthenticationException("The email or password is incorrect.", ExceptionCause.IncorrectInputData);
            }
        }

        public async Task<User> SignUpAsync(SignUp signUp)
        {
            if (!SignUp.IsValidEmail(signUp.Email))
            {
                throw new RegistrationException(
                    $"Cannot register new user. Not valid email address ({signUp.Email}) was given.",
                    ExceptionCause.IncorrectInputData);
            }

            var user = await _UserRepository.GetAsync(signUp.Email);
            if (user != null)
            {
                throw new RegistrationException(
                    $"Cannot register new user. Given Email: {signUp.Email} is already used.",
                    ExceptionCause.IncorrectInputData);
            }

            if (signUp.Password != signUp.ConfirmationPassword)
            {
                throw new RegistrationException(
                    "Cannot register new user. Given Password and Confirmation password does not match.",
                    ExceptionCause.IncorrectInputData);
            }

            var validationResult = await _UserRepository.ValidatePasswordAsync(signUp.Password);
            if (!validationResult.IsValid)
            {
                throw new RegistrationException(
                        "Cannot register new user. Given password is not valid, check details for more information.",
                        validationResult.Errors,
                        ExceptionCause.IncorrectInputData);
            }

            var signUpUser = new User(
                Guid.NewGuid(),
                signUp.Firstname,
                signUp.Lastname,
                signUp.Email
            );

            var createdSuccessfully = await _UserRepository.CreateAsync(signUpUser, signUp.Password);
            
            if (createdSuccessfully)
            {
                var createdUser = await _UserRepository.GetAsync(signUp.Email);
                _AccountsManager.CreateAccount(
                    createdUser,
                    createdUser => {
                        var accountConfirmationToken = _TokenService.GenerateAccountConfirmationTokenAsync(createdUser.Guid).Result;
                        _MailSender.SendConfirmationEmail(createdUser.Email, accountConfirmationToken);
                    },
                    createdUser => {
                        _AccountService.DeleteAccountAsync(createdUser.Guid);
                    });
                return createdUser;
            }
            else
            {
                throw new RegistrationException("User registration failed unexpectedly.");
            }
        }

        public async Task ConfirmAccountAsync(ConfirmAccount confirmAccount)
        {
            try
            {
                var confirmationResult = await _UserRepository
                    .ConfirmationAccountAsync(confirmAccount.User, confirmAccount.ConfirmationToken);

                if (!confirmationResult)
                {
                    throw new AuthenticationException("Invalid token.");
                }
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("Account confirmation failed.", ex);
            }
        }
    }
}
