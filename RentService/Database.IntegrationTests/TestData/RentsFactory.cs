using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.IntegrationTests.TestData
{
    static class RentsFactory
    {
        public static RentEntity GetSampleRentEntity()
        {
            return new RentEntity(
                Guid.NewGuid(),
                new ClientEntity(
                        Guid.NewGuid(),
                        "Client firstname",
                        "Client lastname",
                        "client@email.com"
                    ),
                DateTime.Now,
                DateTime.Now.AddDays(2),
                Vin.FromString("DRT12343256912305"),
                123m
            );
        }

        public static RentEntity GetIncorrectRentEntity()
        {
            return new RentEntity(
                Guid.NewGuid(),
                null,
                DateTime.Now,
                DateTime.Now.AddDays(2),
                Vin.FromString("DRT12343256912305"),
                123m
            );
        }
    }
}
