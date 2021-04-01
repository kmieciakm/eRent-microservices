using Database.Entities;
using Database.Exceptions;
using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Helpers.Mappers
{
    static partial class Mapper
    {
        public static partial class Rent
        {
            public static DbRentEntity MapToDbRentEntity(RentEntity rentEntity)
            {
                try
                {
                    if (rentEntity == null) return null;
                    return new DbRentEntity()
                    {
                        RentGuid = rentEntity.RentGuid,
                        Client = Client.MapToDbClientEntity(rentEntity.Client),
                        ClientGuid = rentEntity.Client.ClientGuid,
                        RentalDate = rentEntity.RentalDate,
                        EndRentalDate = rentEntity.EndRentalDate,
                        RentedVehicleVin = rentEntity.RentedVehicleVin.Value,
                        TotalRentPrice = rentEntity.TotalRentPrice
                    };
                }
                catch (Exception exc)
                {
                    throw new MapperException($"Error while mapping {typeof(RentEntity)} to {typeof(DbRentEntity)}.", exc);
                }
            }

            public static RentEntity MapToRentEntity(DbRentEntity dbRentEntity)
            {
                if (dbRentEntity == null) return null;
                return new RentEntity
                (
                    dbRentEntity.RentGuid,
                    Client.MapToClientEntity(dbRentEntity.Client),
                    dbRentEntity.RentalDate,
                    dbRentEntity.EndRentalDate,
                    Vin.FromString(dbRentEntity.RentedVehicleVin),
                    dbRentEntity.TotalRentPrice
                );
            }

            public static IEnumerable<RentEntity> MapToRentEntity(IEnumerable<DbRentEntity> dbRentEntities)
            {
                return dbRentEntities.Select(dbRent => MapToRentEntity(dbRent));
            }
        }
    }
}
