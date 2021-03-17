using Domain.DomainModels;
using Domain.Ports.Directive;
using Domain.Ports.Infrastructure;
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
        private Client Client { get; set; }
        private List<Rent> ClientRents { get; set; }
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
                .Setup(rent => rent.GetByClient(Client.ClientGuid))
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
            List<Rent> rentExpected = new List<Rent>(ClientRents);
            ICarRentService carRentService = ServicesFactory.CreateCarRentService(ClientRepo, RentRepo);

            Assert.Equal(rentExpected, carRentService.GetClientRents(Client.ClientGuid));
        }
    }
}
