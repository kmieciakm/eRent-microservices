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
        protected TestServer CreateServer()
        {
            var hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:3421")
                .UseStartup<TestStartup>();

            var server = new TestServer(hostBuilder);
            server.BaseAddress = new Uri("http://localhost:3421");

            return server;
        }
    }
}
