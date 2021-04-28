using Domain.Models.Requests;
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
                var signInRequest = new SignInRequest()
                {
                    Email = request.Email,
                    Password = request.Password
                };

                var securityToken = await _AuthenticationService
                    .SignInAsync(signInRequest);

                return new GrpcSignInReply()
                {
                    Token = securityToken
                };
            }
            catch (AuthenticationException authenticationEx)
            {
                _Logger.LogInformation(authenticationEx, authenticationEx.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid authentication credentials."));
            }
            catch (Exception ex)
            {
                var message = "Unexpected error during login.";
                _Logger.LogError(ex, message);
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }
    }
}