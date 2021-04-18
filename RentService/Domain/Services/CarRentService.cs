using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.Exceptions;
using Domain.Ports.Infrastructure.Car;
using Domain.Ports.Infrastructure.Client;
using Domain.Ports.Infrastructure.Rent;
using Domain.Ports.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    class CarRentService : ICarRentService
    {
        private IRentQuery _RentQuery { get; }
        private IRentCreator _RentCreator { get; }
        private IClientQuery _ClientQuery { get; }
        private ICarQuery _CarQuery { get; }

        public CarRentService(
            IRentQuery rent, IRentCreator rentCreator,
            IClientQuery clientQuery,
            ICarQuery carQuery)
        {
            _RentQuery = rent;
            _RentCreator = rentCreator;
            _ClientQuery = clientQuery;
            _CarQuery = carQuery;
        }

        public IList<RentEntity> GetClientRentals(Guid clientGuid)
        {
            return _RentQuery.GetRentsOfClient(clientGuid);
        }

        public RentEntity RentCar(Guid clientGuid, Vin carVin, DateTime startRentDate, DateTime endRentDate)
        {
            var client = _ClientQuery.GetClient(clientGuid);
            if (client == null)
            {
                throw new RentException("Cannot rent a car. Given client does not exists.", clientGuid, carVin);
            }

            if (!_CarQuery.Exist(carVin))
            {
                throw new RentException("Cannot rent a car. Given car does not exists.", clientGuid, carVin);
            }

            if (DateTime.Compare(startRentDate, DateTime.Now) < 0)
            {
                throw new RentException("Cannot rent a car. The initial rental date has already expired.", clientGuid, carVin);
            }

            try
            {
                var rentPrice = CalculateBaseRentPrice(carVin, startRentDate, endRentDate);

                var newRentalGuid = Guid.NewGuid();
                var rentalToCreate = new RentEntity(
                        newRentalGuid,
                        client,
                        startRentDate,
                        endRentDate,
                        carVin,
                        rentPrice
                    );
                _RentCreator.Create(rentalToCreate);
                return _RentQuery.GetRent(newRentalGuid);
            }
            catch (Exception exception)
            {
                throw new RentException("Cannot rent a car.", exception, clientGuid, carVin);
            }
        }

        // TODO: Move to separate service responsible for pricing
        private decimal CalculateBaseRentPrice(Vin carVin, DateTime startRentDate, DateTime endRentDate)
        {
            var car = _CarQuery.Get(carVin);
            var totalRentDays = Math.Ceiling((endRentDate - startRentDate).TotalDays);
            var rentDays = Convert.ToInt64(totalRentDays);

            return rentDays * car.PricePerDay;
        }
    }
}
