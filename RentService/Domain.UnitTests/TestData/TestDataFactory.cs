using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.UnitTests.TestData
{
    /// <summary>
    /// Supply reusable data for tests.
    /// </summary>
    static class TestDataFactory
    {
        public static (ClientEntity client, List<RentEntity> rents) GetClientWithItsRents()
        {
            var client = new ClientEntity(
                Guid.NewGuid(),
                "Test_Firstname",
                "Test_Lastname",
                "Test_Email"
            );
            var rents = new List<RentEntity>() {
                new RentEntity(
                    Guid.NewGuid(),
                    client,
                    DateTime.Now,
                    DateTime.Now.AddDays(7),
                    Vin.FromString("12345678901234567"),
                    800m
                ),
                new RentEntity(
                    Guid.NewGuid(),
                    client,
                    DateTime.Now,
                    DateTime.Now.AddDays(2),
                    Vin.FromString("72345678901234561"),
                    90m
                )
            };

            return (client, rents);
        }
    }
}
