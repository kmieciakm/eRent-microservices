using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.FunctionalTests.Fixture;
using Xunit;

namespace Web.FunctionalTests.Cases
{
    public class RentsControllerTests : BaseTestFixture
    {
        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("8d0da683-8442-4972-aa75-c374f6759357")]
        [InlineData("68ddc59d-2eeb-4a4e-b037-78f4e69be389")]
        public async Task Get_Rents_NotFound(Guid rentGuid)
        {
            using (var server = CreateServer())
            {
                var request = $"api/rents/{rentGuid}";
                var response = await server.CreateClient().GetAsync(request);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("11111111-1111-1111-1111-222222222222")]
        [InlineData("22222222-2222-2222-2222-222222222222")]
        public async Task Get_Rents_Ok(Guid rentGuid)
        {
            using (var server = CreateServer())
            {
                var request = $"/api/rents/{rentGuid}";
                var response = await server.CreateClient().GetAsync(request);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
