using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.Ports.Infrastructure.Car;
using Domain.Ports.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    class CarService : ICarService
    {
        private ICarQuery _CarQuery { get; }

        public CarService(ICarQuery carQuery)
        { 
            _CarQuery = carQuery;
        }

        public CarEntity GetCar(Vin vin)
        {
            return _CarQuery.Get(vin);
        }
    }
}
