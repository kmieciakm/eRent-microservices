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
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Authentication.AuthenticationClient(channel);
            var request = new GrpcSignInRequest()
            {
                Email = "test@localhost.com",
                Password = "QWERTY"
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
            Console.ReadLine();
        }
    }
}
