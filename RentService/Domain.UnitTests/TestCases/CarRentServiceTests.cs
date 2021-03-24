using Domain.DomainModels;
using Domain.Ports.Infrastructure;
using Domain.Ports.Presenters;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Domain.UnitTests.TestCases
{
    public class CarRentServiceTests
    {
        private ClientEntity Client { get; set; }
        private List<RentEntity> ClientRents { get; set; }
        private IClient ClientRepo { get; set; }
        private IRent RentRepo { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public CarRentServiceTests()
        {
            (Client, ClientRents) = TestData.TestDataFactory.GetClientWithItsRents();

            Mock<IClient> mockClient = new Mock<IClient>();
            Mock<IRent> rentMock = new Mock<IRent>();

            mockClient
                .Setup(client => client.Get(Client.ClientGuid))
                .Returns(Client);
            rentMock
                .Setup(rent => rent.GetRentsOfClient(Client.ClientGuid))
                .Returns(ClientRents);

            ClientRepo = mockClient.Object;
            RentRepo = rentMock.Object;
        }

        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetClientRents_ClientHasRents()
        {
            List<RentEntity> rentExpected = new List<RentEntity>(ClientRents);
            ICarRentService carRentService = ServicesFactory.CreateCarRentService(ClientRepo, RentRepo);

            Assert.Equal(rentExpected, carRentService.GetClientRents(Client.ClientGuid));
        }
    }
}
