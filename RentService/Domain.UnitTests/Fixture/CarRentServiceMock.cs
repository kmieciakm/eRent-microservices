using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.Ports.Infrastructure.Car;
using Domain.Ports.Infrastructure.Client;
using Domain.Ports.Infrastructure.Rent;
using Domain.Ports.Presenters;
using Domain.Services;
using Domain.UnitTests.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.UnitTests.Fixture
{
    /// <summary>
    /// Provides ICarRentService with mock infrastructure along with data used to mock the service.
    /// </summary>
    class CarRentServiceMock
    {
        public ClientEntity Client { get; private set; }
        public List<RentEntity> Rents { get; private set; }
        public List<CarEntity> Cars { get; private set; }
        public ICarRentService CarRentService { get; }

        public CarRentServiceMock()
        {
            (Client, Rents, Cars) = TestDataFactory.GetRentDataSet();

            MockServices(
                out IRentQuery rentQuery,
                out IClientQuery clientQuery,
                out ICarQuery carQuery,
                out IRentCreator rentCreate);

            CarRentService = new CarRentService(rentQuery, rentCreate, clientQuery, carQuery);
        }

        private void MockServices(out IRentQuery rentQuery, out IClientQuery clientQuery, out ICarQuery carQuery, out IRentCreator rentCreate)
        {
            Mock<IRentQuery> rentMock = new Mock<IRentQuery>();
            rentMock
                .Setup(rent => rent.GetRentsOfClient(Client.ClientGuid))
                .Returns(Rents);
            rentMock
                .Setup(rent => rent.GetRent(It.IsAny<Guid>()))
                .Returns<Guid>(rentGuid => Rents.FirstOrDefault(rent => rent.RentGuid.Equals(rentGuid)));

            Mock<IClientQuery> clientMock = new Mock<IClientQuery>();
            clientMock
                .Setup(client => client.GetClient(Client.ClientGuid))
                .Returns(Client);

            Mock<ICarQuery> carQueryMock = new Mock<ICarQuery>();
            carQueryMock
                .Setup(car => car.Get(It.IsAny<Vin>()))
                .Returns<Vin>(vin => Cars.FirstOrDefault(car => car.Vin.Equals(vin)));
            carQueryMock
                .Setup(car => car.Exist(It.IsAny<Vin>()))
                .Returns<Vin>(vin => Cars.Any(car => car.Vin.Equals(vin)));

            Mock<IRentCreator> rentCreateMock = new Mock<IRentCreator>();
            rentCreateMock
                .Setup(rent => rent.Create(It.IsAny<RentEntity>()))
                .Callback<RentEntity>(rent => Rents.Add(rent));

            rentQuery = rentMock.Object;
            clientQuery = clientMock.Object;
            carQuery = carQueryMock.Object;
            rentCreate = rentCreateMock.Object;
        }
    }
}
