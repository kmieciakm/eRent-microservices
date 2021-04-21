using SOAP.FunctionalTests.Fixture;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SOAP.FunctionalTests.Cases
{
    public class RentTests : BaseTestFixture
    {
        [Fact]
        public void CreateServer_Check()
        {
            using (var server = CreateServer())
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("11111111-1111-1111-1111-111111111111")]
        [InlineData("22222222-2222-2222-2222-222222222222")]
        public async Task Get_Rents_Ok(Guid rentGuid)
        {
            using (var server = CreateServer())
            {
                var url = Services.RentServiceUrl;
                var action = "GetRentalsOfClient";
                var parameters = new Dictionary<string, string>()
                {
                    { "clientId", rentGuid.ToString() }
                };

                string envelope = SoapHelper.GetEnvelope(action, parameters);

                var response = await server.CreateClient().PostAsync(url, new StringContent(envelope));
                var result = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
