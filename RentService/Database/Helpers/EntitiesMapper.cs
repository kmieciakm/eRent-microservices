using Database.Entities;
using Database.Exceptions;
using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Helpers
{
    static class EntitiesMapper
    {
        public static DbClientEntity MapToDbClientEntity(ClientEntity clientEntity)
        {
            if (clientEntity == null) return null;
            return new DbClientEntity()
            {
                ClientGuid = clientEntity.ClientGuid,
                Firstname = clientEntity.Firstname,
                Lastname = clientEntity.Lastname,
                Email = clientEntity.Email
            };
        }

        public static ClientEntity MapToClientEntity(DbClientEntity dbClientEntity)
        {
            if (dbClientEntity == null) return null;
            return new ClientEntity
            (
                dbClientEntity.ClientGuid,
                dbClientEntity.Firstname,
                dbClientEntity.Lastname,
                dbClientEntity.Email
            );
        }

        public static DbRentEntity MapToDbRentEntity(RentEntity rentEntity)
        {
            try
            {
                if (rentEntity == null) return null;
                return new DbRentEntity()
                {
                    RentGuid = rentEntity.RentGuid,
                    Client = MapToDbClientEntity(rentEntity.Client),
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
                MapToClientEntity(dbRentEntity.Client),
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
