using Database.IntegrationTests.TestData;
using Domain.Ports.Infrastructure.Client;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Database.IntegrationTests.TestCases
{
    public class ClientRepositoryTests
    {
        private IClientQuery _Client { get; set; }
        private IClientCreate _ClientCreate { get; set; }
        private IClientDelete _ClientDelete { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public ClientRepositoryTests(IClientQuery clientQuery, IClientCreate clientCreate, IClientDelete clientDelete)
        {
            _Client = clientQuery;
            _ClientCreate = clientCreate;
            _ClientDelete = clientDelete;
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
            var createdCorrectly = _ClientCreate.Create(client);

            Assert.True(createdCorrectly);
            Assert.Equal(client, _Client.Get(client.ClientGuid));
        }

        [Fact]
        public void CreateClient_ClientDataIncorrect()
        {
            var client = ClientFactory.GetIncorrectClientEntity();

            Assert.Throws<DbUpdateException>(() =>
                _ClientCreate.Create(client)
            );
        }

       [Fact]
        public void DeleteClient_ClientDataCorrect()
        {
            var client = ClientFactory.GetSampleClientEntity();
            _ClientCreate.Create(client);

            var deletedCorrectly = _ClientDelete.Delete(client.ClientGuid);

            Assert.True(deletedCorrectly);
            Assert.Null(_Client.Get(client.ClientGuid));
        }

        [Fact]
        public void DeleteClient_ClientDoesNotExist()
        {
            var clientGuid = Guid.NewGuid();

            var deletedCorrectly = _ClientDelete.Delete(clientGuid);

            Assert.False(deletedCorrectly);
        }

        [Fact]
        public void GetClient_ClientDataCorrect()
        {
            var client = ClientFactory.GetSampleClientEntity();

            var createdCorrectly = _ClientCreate.Create(client);

            Assert.True(createdCorrectly);
            Assert.Equal(client, _Client.Get(client.ClientGuid));
        }
    }
}
