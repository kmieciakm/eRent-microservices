using Database.Adapters;
using Database.DatabaseContext;
using Database.Repositories;
using Domain.DomainModels;
using Domain.Ports.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Database.IntegrationTests
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
            var client = new ClientEntity(
                Guid.NewGuid(),
                "Test Firstname",
                "Test Lastname",
                "correctEmail@address.com"
            );

            var createdCorrectly = _Client.Create(client);
            Assert.True(createdCorrectly);
        }

        [Fact]
        public void CreateClient_ClientDataIncorrect()
        {
            var client = new ClientEntity(
                Guid.NewGuid(),
                "Test Firstname",
                "Test Lastname",
                null
            );

            Assert.Throws<DbUpdateException>(() =>
                _Client.Create(client)
            );
        }

        [Fact]
        public void DeleteClient_ClientDataCorrect()
        {
            var clientGuid = Guid.NewGuid();
            var client = new ClientEntity(
               clientGuid,
               "Test Firstname",
               "Test Lastname",
               "correctEmail@address.com"
           );
            _Client.Create(client);
            _Client.Delete(clientGuid);

            Assert.Null(_Client.Get(clientGuid));
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
            var clientGuid = Guid.NewGuid();
            var client = new ClientEntity(
               clientGuid,
               "Test Firstname",
               "Test Lastname",
               "correctEmail@address.com"
           );
            _Client.Create(client);
            _Client.Get(clientGuid);

            Assert.Equal(client, _Client.Get(clientGuid));
        }
    }
}
