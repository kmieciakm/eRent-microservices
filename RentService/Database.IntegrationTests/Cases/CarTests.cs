using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.Ports.Infrastructure.Car;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Database.IntegrationTests.Cases
{
    public class CarTests
    {
        private ICarQuery _Car { get; set; }
        private ICarCreate _CarCreate { get; set; }
        private ICarModify _CarModify { get; set; }
        private ICarDelete _CarDelete { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public CarTests(ICarQuery carQuery, ICarCreate carCreate, ICarModify carModify, ICarDelete carDelete)
        {
            _Car = carQuery;
            _CarCreate = carCreate;
            _CarModify = carModify;
            _CarDelete = carDelete;
        }

        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetCar_CarDataCorret()
        {
            var carExpected = _Car.Get(Vin.FromString("C3333333333333333"));
            var car = _Car.Get(carExpected.Vin);

            Assert.Equal(carExpected, car);
        }

        [Fact]
        public void CreateCar_CarDataCorrect()
        {
            var car = new CarEntity(
                Vin.FromString("DRT12343256912305"),
                "toyota",
                "yaris",
                100000,
                20,
                1999
            );
            var createdCorrectly = _CarCreate.Create(car);

            Assert.True(createdCorrectly);
            Assert.Equal(car, _Car.Get(car.Vin));
        }

        [Fact]
        public void CreateCar_CarDataIncorrect()
        {
            var car = new CarEntity(
               Vin.FromString("DRT12343256912005"),
               null,
               "yaris",
               100000,
               20,
               1999
           );

            Assert.Throws<DbUpdateException>(() =>
                _CarCreate.Create(car)
            );
        }

        [Fact]
        public void UpdateCar_CarDataCorrect()
        {
            var carVin = Vin.FromString("C3333333333333333");
            var car = _Car.Get(carVin);
            car.Mileage = 120000;
            var updatedCorrectly = _CarModify.Update(car);

            Assert.True(updatedCorrectly);
            Assert.Equal(car, _Car.Get(car.Vin));
        }

        [Fact]
        public void DeleteCar_CarDataCorrect()
        {
            var carVin = Vin.FromString("C2222222222222222");
            var deletedCorrectly = _CarDelete.Delete(carVin);

            Assert.True(deletedCorrectly);
            Assert.Null(_Car.Get(carVin));
        }

        [Fact]
        public void DeleteCar_CarDoesNotExist()
        {
            var carVin = Vin.FromString("Crdt22222222222ty"); ;
            var deletedCorrectly = _CarDelete.Delete(carVin);

            Assert.False(deletedCorrectly);
        }
    }
}