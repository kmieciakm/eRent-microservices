using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.UnitTests.Data
{
    /// <summary>
    /// Supply reusable data for tests.
    /// </summary>
    static class TestDataFactory
    {
        public static (ClientEntity client, List<RentEntity> rents, List<CarEntity> cars) GetRentDataSet()
        {
            var client = new ClientEntity(
                Guid.NewGuid(),
                "Nathaniel",
                "Flynn",
                "dignissim@tempor.net"
            );
            var rents = new List<RentEntity>() {
                new RentEntity(
                    Guid.NewGuid(),
                    client,
                    DateTime.Now,
                    DateTime.Now.AddDays(7),
                    Vin.FromString("33333333333333333"),
                    940m
                ),
                new RentEntity(
                    Guid.NewGuid(),
                    client,
                    DateTime.Now,
                    DateTime.Now.AddDays(2),
                    Vin.FromString("22222222222222222"),
                    140m
                )
            };
            var cars = new List<CarEntity>() {
                new CarEntity(
                    Vin.FromString("33333333333333333"),
                    "Fiat", "Bravo", 130000, 120m, 2010
                ),
                new CarEntity(
                    Vin.FromString("22222222222222222"),
                    "Toyota", "Yaris", 150000, 70m, 2003
                ),
                new CarEntity(
                    Vin.FromString("11111111111111111"),
                    "BMW", "X3", 30000, 300m, 2019
                )
            };

            return (client, rents, cars);
        }
    }
}
