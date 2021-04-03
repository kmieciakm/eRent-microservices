using Domain.DomainModels;
using Domain.Ports.Infrastructure.Rent;
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
        private ICarRentService RentService { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public CarRentServiceTests()
        {
            (Client, ClientRents) = TestData.TestDataFactory.GetClientWithItsRents();

            Mock<IRentQuery> rentMock = new Mock<IRentQuery>();
            rentMock
                .Setup(rent => rent.GetRentsOfClient(Client.ClientGuid))
                .Returns(ClientRents);

            var rentQuery = rentMock.Object;
            RentService = ServicesFactory.CreateCarRentService(rentQuery);
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
            Assert.Equal(rentExpected, RentService.GetClientRents(Client.ClientGuid));
        }
    }
}
