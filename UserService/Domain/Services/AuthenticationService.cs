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

        public AuthenticationService(IUserRegistry userRepository, ITokenService tokenService)
        {
            _UserRepository = userRepository;
            _TokenService = tokenService;
        }

        public Task<User> GetIdentity(string email)
        {
            return _UserRepository.GetAsync(email);
        }

        public async Task<string> SignInAsync(SignIn signIn)
        {
            // TODO: Check if user confirmed account
            var authenticated = await _UserRepository
                .AuthenticateAsync(signIn.Email, signIn.Password);

            if (authenticated)
            {
                return _TokenService.GenerateSecurityToken(signIn.Email);
            }
            else
            {
                throw new AuthenticationException("The email or password is incorrect.");
            }
        }

        public async Task<User> SignUpAsync(SignUp signUp)
        {
            if (signUp.Password != signUp.ConfirmationPassword)
            {
                throw new RegistrationException(
                    "Cannot register new user. Given Password and Confirmation password does not match.",
                    ExceptionCause.IncorrectInputData);
            }

            // TODO: Check if email is not used

            var signUpUser = new User()
            {
                Name = signUp.Name,
                Email = signUp.Email
            };

            // TODO: Validate password policy

            var createdSuccessfully = await _UserRepository.CreateAsync(signUpUser, signUp.Password);
            if (createdSuccessfully)
            {
                var createdUser = await _UserRepository.GetAsync(signUp.Email);
                var accountConfirmationToken = _UserRepository.GenerateAccountConfirmationTokenAsync(createdUser);
                // TODO: Send confirmation email

                return createdUser;
            }
            else
            {
                throw new RegistrationException("User registration failed unexpectedly.");
            }
        }

        public void ConfirmAccountAsync(ConfirmAccount confirmation)
        {
            try
            {
                var confirmationResult = _UserRepository
                    .ConfirmationAccountAsync(confirmation.User, confirmation.ConfirmationToken);
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("Account confirmation failed.", ex);
            }
        }
    }
}
