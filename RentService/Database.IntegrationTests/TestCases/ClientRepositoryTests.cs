using Database.Adapters;
using Database.DatabaseContext;
using Database.IntegrationTests.TestData;
using Database.Repositories;
using Domain.Ports.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Database.IntegrationTests.TestCases
{
    public class ClientRepositoryTests
    {
        private IClient _Client { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// Tests run slower, but data separation for each test is ensured.
        /// </remarks>
        public ClientRepositoryTests()
        {
            var dbContext = DbContextFactory
                .CreateInMemoryRentDatabase()
                .CreateDbContext();

            var dbContextSeed = new RentDbContextSeed(dbContext);
            dbContextSeed.SeedData();

            _Client = new ClientAdapter(
                new ClientRepository(dbContext)
            );
        }

        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void CreateClient_ClientDataCorrect()
        {
            var client = ClientFactory.GetSampleClientEntity();
            var createdCorrectly = _Client.Create(client);

            Assert.True(createdCorrectly);
        }

        [Fact]
        public void CreateClient_ClientDataIncorrect()
        {
            var client = ClientFactory.GetIncorrectClientEntity();

            Assert.Throws<DbUpdateException>(() =>
                _Client.Create(client)
            );
        }

        [Fact]
        public void DeleteClient_ClientDataCorrect()
        {
            var client = ClientFactory.GetSampleClientEntity();
            _Client.Create(client);
            _Client.Delete(client.ClientGuid);

            Assert.Null(_Client.Get(client.ClientGuid));
        }

        [Fact]
        public void DeleteClient_ClientDataIncorrect()
        {
            var clientGuid = Guid.NewGuid();
            var deletedCorrectly = _Client.Delete(clientGuid);

            Assert.False(deletedCorrectly);
        }

        [Fact]
        public void GetClient_ClientDataCorrect()
        {
            var client = ClientFactory.GetSampleClientEntity();
            _Client.Create(client);
            _Client.Get(client.ClientGuid);

            Assert.Equal(client, _Client.Get(client.ClientGuid));
        }
    }
}
