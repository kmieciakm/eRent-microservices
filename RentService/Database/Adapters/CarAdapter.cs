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
    class CarAdapter : ICarQuery, ICarCreate, ICarDelete, ICarModify
    {
        private ICarRepository _CarRepository { get; set; }

        public CarAdapter(ICarRepository carRepository)
        {
            _CarRepository = carRepository;
        }

        bool ICarQuery.Exist(Vin carVin)
        {
            return _CarRepository.Exist(carVin);
        }

        CarEntity ICarQuery.Get(Vin carVin)
        {
            var dbCar = _CarRepository.Get(carVin);
            return Mapper.Car.MapToCarEntity(dbCar);
        }

        bool ICarCreate.Create(CarEntity car)
        {
            var dbCar = Mapper.Car.MapToDbCarEntity(car);
            return _CarRepository.CreateAndSave(dbCar);
        }

        bool ICarModify.Update(CarEntity car)
        {
            var dbCar = Mapper.Car.MapToDbCarEntity(car);
            return _CarRepository.UpdateAndSave(dbCar);
        }

        bool ICarDelete.Delete(Vin vin)
        {
            return _CarRepository.DeleteAndSave(vin);
        }
    }
}
