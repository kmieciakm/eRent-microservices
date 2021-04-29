using Domain.Infrastructure;
using Domain.Models;
using Domain.Models.Requests;
using Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
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

        public async Task<string> SignInAsync(SignInRequest signInRequest)
        {
            // TODO: Check if user confirmed account

            var authenticated = await _UserRepository
                .AuthenticateAsync(signInRequest.Email, signInRequest.Password);

            if (authenticated)
            {
                return _TokenService.GenerateSecurityToken(signInRequest.Email);
            }
            else
            {
                throw new AuthenticationException("The email or password is incorrect.");
            }
        }

        public async Task<User> SignUpAsync(SignUpRequest signUpRequest)
        {
            if (signUpRequest.Password != signUpRequest.ConfirmationPassword)
            {
                throw new RegistrationException("Cannot register new user. Given Password and Confirmation password does not match.");
            }

            // TODO: Check if email is not used

            var signUpUser = new User()
            {
                Name = signUpRequest.Name,
                Email = signUpRequest.Email
            };

            // TODO: Validate password policy

            var createdSuccessfully = await _UserRepository.CreateAsync(signUpUser, signUpRequest.Password);
            if (createdSuccessfully)
            {
                var createdUser = await _UserRepository.GetAsync(signUpRequest.Email);
                var accountConfirmationToken = _UserRepository.GenerateAccountConfirmationTokenAsync(createdUser);
                // TODO: Send confirmation email

                return createdUser;
            }
            else
            {
                throw new RegistrationException("User registration failed.");
            }
        }

        public void ConfirmAccountAsync(ConfirmAccountRequest confirmationRequest)
        {
            try
            {
                var confirmationResult = _UserRepository
                    .ConfirmationAccountAsync(confirmationRequest.User, confirmationRequest.ConfirmationToken);
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("Account confirmation failed.", ex);
            }
        }
    }
}
