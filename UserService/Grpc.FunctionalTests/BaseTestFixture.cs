using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.FunctionalTests
{
    public abstract class BaseTestFixture
    {
        protected static TestServer CreateServer()
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder
                        .AddJsonFile("appsettings.Development.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseStartup<TestStartup>()
                .UseEnvironment("Development");

            return new TestServer(hostBuilder)
            {
                PreserveExecutionContext = true,
                AllowSynchronousIO = true,
                BaseAddress = new Uri(@"https://localhost:5001")
            };
        }
    }
}
