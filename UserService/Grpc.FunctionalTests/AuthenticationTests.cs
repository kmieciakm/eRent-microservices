using Grpc.FunctionalTests.Protos;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Grpc.FunctionalTests
{
    public class AuthenticationTests : BaseTestFixture
    {
        [Fact]
        public void Init_TestServer()
        {
            using (var host = CreateServer())
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task Authenticate_CredentialsCorrent()
        {
            using (var server = CreateServer())
            {
                var client = server.Services
                    .GetRequiredService<Authentication.AuthenticationClient>();
                
                var request = new GrpcSignInRequest()
                {
                    Email = "test@localhost.com",
                    Password = "QWERTY"
                };

                GrpcSignInReply response = await client.SignInAsync(request);
                Assert.NotNull(response.Token);
            }
        }

        [Fact]
        public void Authenticate_CredentialsIncorrent()
        {
            using (var server = CreateServer())
            {
                var client = server.Services
                    .GetRequiredService<Authentication.AuthenticationClient>();

                var request = new GrpcSignInRequest()
                {
                    Email = "test@localhost.com",
                    Password = "Admin123"
                };

                _ = Assert.ThrowsAsync<AuthenticationException>(async () =>
                        await client.SignInAsync(request));
            }
        }
    }
}
