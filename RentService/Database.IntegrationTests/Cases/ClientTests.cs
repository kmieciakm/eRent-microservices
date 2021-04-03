using Domain.DomainModels;
using Domain.Ports.Infrastructure.Client;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Database.IntegrationTests.TestCases
{
    public class ClientTests
    {
        private IClientQuery _Client { get; set; }
        private IClientCreate _ClientCreate { get; set; }
        private IClientModify _ClientModify { get; set; }
        private IClientDelete _ClientDelete { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public ClientTests(IClientQuery clientQuery, IClientCreate clientCreate, IClientModify clientModify, IClientDelete clientDelete)
        {
            _Client = clientQuery;
            _ClientCreate = clientCreate;
            _ClientModify = clientModify;
            _ClientDelete = clientDelete;
        }

        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetClient_ClientDataCorrect()
        {
            var clientGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var client = _Client.Get(clientGuid);

            Assert.Equal(clientGuid, client.ClientGuid);
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
            var createdCorrectly = _ClientCreate.Create(client);

            Assert.True(createdCorrectly);
            Assert.Equal(client, _Client.Get(client.ClientGuid));
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
                _ClientCreate.Create(client)
            );
        }

        [Fact]
        public void UpdateClient_ClientDataCorrect()
        {
            var clientGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var client = _Client.Get(clientGuid);
            client.Email = "brandNewEmail@email.com";
            var updatedCorrectly = _ClientModify.Update(client);

            Assert.True(updatedCorrectly);
            Assert.Equal(client, _Client.Get(client.ClientGuid));
        }

        [Fact]
        public void DeleteClient_ClientDataCorrect()
        {
            var clientGuid = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var deletedCorrectly = _ClientDelete.Delete(clientGuid);

            Assert.True(deletedCorrectly);
            Assert.Null(_Client.Get(clientGuid));
        }

        [Fact]
        public void DeleteClient_ClientDoesNotExist()
        {
            var clientGuid = Guid.NewGuid();
            var deletedCorrectly = _ClientDelete.Delete(clientGuid);

            Assert.False(deletedCorrectly);
        }
    }
}
