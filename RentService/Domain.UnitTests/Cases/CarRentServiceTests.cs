using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.UnitTests.Fixture;
using Domain.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.UnitTests.Cases
{
    public class CarRentServiceTests
    {
        private CarRentServiceMock RentServiceMock { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public CarRentServiceTests()
        {
            RentServiceMock = new CarRentServiceMock();
        }

        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetClientRents_ClientHasRents()
        {
            var rentsExpected = new List<RentEntity>(RentServiceMock.Rents);
            var rents = RentServiceMock.CarRentService.GetClientRentals(RentServiceMock.Client.ClientGuid);

            Assert.Equal(rentsExpected, rents);
        }

        [Fact]
        public void RentCar_Correct()
        {
            var clientGuid = RentServiceMock.Client.ClientGuid;
            var carVin = Vin.FromString("11111111111111111");
            var startRentalDate = DateTime.Now.AddHours(1);
            var endRentalDate = startRentalDate.AddDays(2);

            var expectedRental = new RentEntity(new Guid(), RentServiceMock.Client, startRentalDate, endRentalDate, carVin, 600m);
            var rental = RentServiceMock.CarRentService.RentCar(clientGuid, carVin, startRentalDate, endRentalDate);

            Assert.Equal(expectedRental, rental, new RentValuesComparator());
        }
    }
}
