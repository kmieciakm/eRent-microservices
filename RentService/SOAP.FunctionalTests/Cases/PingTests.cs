using Microsoft.AspNetCore.Hosting;
using SOAP.FunctionalTests.Fixture;
using SOAP.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SOAP.FunctionalTests.Cases
{
    public class PingTests : BaseTestFixture
    {
        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void CreateServer_Check()
        {
            using (var server = CreateServer())
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task Ping_Correct()
        {
            using (var server = CreateServer())
            {
                var url = @"http://localhost:3421/PingService.asmx";
                var action = "Ping";
                var parameters = new Dictionary<string, string>()
                {
                    { "msg", "test" }
                };

                string envelope = SoapHelper.GetEnvelope(action, parameters);

                var response = await server.CreateClient().PostAsync(url, new StringContent(envelope));
                var result = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
