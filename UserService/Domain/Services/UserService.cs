using Domain.Infrastructure;
using Domain.Models;
using Domain.Models.Requests;
using Domain.Models.Response;
using Domain.Services.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    class UserService : IUserService
    {
        private IUserRegistry _UserRepository { get; }
        private IOptions<AuthenticationSettings> _AuthenticationSettings { get; }

        public UserService(IUserRegistry userRepository, IOptions<AuthenticationSettings> authentictionSettings)
        {
            _UserRepository = userRepository;
            _AuthenticationSettings = authentictionSettings;
        }

        public async Task<TokenResponse> SignInAsync(SignInRequest signInRequest)
        {
            var authenticationResult = await _UserRepository
                .AuthenticateAsync(signInRequest.Email, signInRequest.Password);
            // TODO: Generate Token

            throw new NotImplementedException();
        }

        public async Task<User> SignUpAsync(SignUpRequest signUpRequest)
        {
            if (signUpRequest.Password != signUpRequest.ConfirmationPassword)
            {
                throw new RegistrationException("Cannot register new user. Given Password and Confirmation password does not match.");
            }

            var signUpUser = new User()
            {
                Name = signUpRequest.Name,
                Email = signUpRequest.Email
            };

            var createdSuccessfully = await _UserRepository.CreateAsync(signUpUser, signUpRequest.Password);
            if (createdSuccessfully)
            {
                return await _UserRepository.GetByEmailAsync(signUpRequest.Email);
            }
            else
            {
                throw new RegistrationException("Un unexpected error occurred during registration.");
            }
        }
    }
}
