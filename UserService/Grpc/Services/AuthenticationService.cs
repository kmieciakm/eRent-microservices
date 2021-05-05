using Domain.Exceptions;
using Domain.Models;
using Domain.Services.Contracts;
using Grpc.Core;
using Grpc.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Grpc.Services
{
    public class GrpcAuthenticationService : Authentication.AuthenticationBase
    {
        private ILogger<GrpcAuthenticationService> _Logger { get; }
        private IAuthenticationService _AuthenticationService { get; }

        public GrpcAuthenticationService(ILogger<GrpcAuthenticationService> logger, IAuthenticationService authenticationService)
        {
            _Logger = logger;
            _AuthenticationService = authenticationService;
        }

        public override async Task<GrpcSignInReply> SignIn(GrpcSignInRequest request, ServerCallContext context)
        {
            try
            {
                var signIn = new SignIn()
                {
                    Email = request.Email,
                    Password = request.Password
                };

                var securityToken = await _AuthenticationService
                    .SignInAsync(signIn);

                return new GrpcSignInReply()
                {
                    Token = securityToken
                };
            }
            catch (Domain.Exceptions.AuthenticationException authenticationEx)
            {
                _Logger.LogInformation(authenticationEx, authenticationEx.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, authenticationEx.Message));
            }
            catch (Exception ex)
            {
                var message = "Unexpected error during login.";
                _Logger.LogError(ex, message);
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }

        public override async Task<GrpcSignUpReply> SignUp(GrpcSignUpRequest request, ServerCallContext context)
        {
            try
            {
                var signUp = new SignUp()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password,
                    ConfirmationPassword = request.PasswordConfirmation
                };

                var createdUser = await _AuthenticationService
                    .SignUpAsync(signUp);

                return new GrpcSignUpReply()
                {
                    Name = createdUser.Name,
                    Email = createdUser.Email
                };
            }
            catch (RegistrationException registrationEx)
            {
                _Logger.LogInformation(registrationEx, registrationEx.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, registrationEx.Message));
            }
            catch (Exception ex)
            {
                var message = "Unexpected error during registration.";
                _Logger.LogError(ex, message);
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }
    }
}