using Database.Helpers.Mappers;
using Database.Repositories.Contracts;
using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.Ports.Infrastructure.Car;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Adapters
{
    class CarAdapter : ICarQuery
    {
        private ICarRepository _CarRepository { get; set; }

        public CarAdapter(ICarRepository carRepository)
        {
            _CarRepository = carRepository;
        }

        public bool Exist(Vin carVin)
        {
            return _CarRepository.Exist(carVin);
        }

        public CarEntity Get(Vin carVin)
        {
            var dbCar = _CarRepository.Get(carVin);
            return Mapper.Car.MapToCarEntity(dbCar);
        }
    }
}
