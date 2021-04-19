using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.FunctionalTests.Fixture
{
    public abstract class BaseTestFixture
    {
        protected TestServer CreateServer()
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseEnvironment("Testing")
                .UseStartup<TestStartup>();

            return new TestServer(hostBuilder);
        }
    }
}
