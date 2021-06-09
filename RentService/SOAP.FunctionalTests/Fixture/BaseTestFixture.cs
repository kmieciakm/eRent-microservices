using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOAP.FunctionalTests.Fixture
{
    public abstract class BaseTestFixture
    {
        private static string BaseUrl { get; } = @"http://localhost:3421";

        protected TestServer CreateServer()
        {
            var hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:3421")
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder
                        .AddJsonFile("appsettings.Testing.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseStartup<TestStartup>();

            var server = new TestServer(hostBuilder);
            server.BaseAddress = new Uri(BaseUrl);

            return server;
        }

        protected static class Services
        {
            public static string PingServiceUrl { get; } = $"{BaseUrl}/PingService.asmx";
            public static string RentServiceUrl { get; } = $"{BaseUrl}/RentService.asmx";
        }
    }
}
