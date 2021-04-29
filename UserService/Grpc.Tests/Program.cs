using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tests.Protos;
using System;
using System.Threading.Tasks;

namespace Grpc.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--------------------");
            await TryRegisterAsync();
            Console.WriteLine("--------------------");
            await TryLoginAsync();

            Console.ReadLine();
        }

        private static Authentication.AuthenticationClient CreateClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            return new Authentication.AuthenticationClient(channel);
        }

        private static async Task TryLoginAsync()
        {
            var client = CreateClient();
            var request = new GrpcSignInRequest()
            {
                Email = "newuser@localhost.com",
                Password = "Qwerty123_"
            };

            try
            {
                var response = await client.SignInAsync(request);
                var token = response.Token;
                Console.WriteLine(token);
            }
            catch (RpcException rpcEx)
            {
                Console.WriteLine(rpcEx.Message);
            }
        }

        private static async Task TryRegisterAsync()
        {
            var client = CreateClient();
            var request = new GrpcSignUpRequest()
            {
                Name = "TestName",
                Email = "newuser@localhost.com",
                Password = "Qwerty123_",
                PasswordConfirmation = "Qwerty123_"
            };

            try
            {
                var response = await client.SignUpAsync(request);
                Console.WriteLine($"Created user: {response.Name} / {response.Email}");
            }
            catch (RpcException rpcEx)
            {
                Console.WriteLine(rpcEx.Message);
            }
        }
    }
}
