using Database.Entities;
using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Helpers.Mappers
{
    static partial class Mapper
    {
        public static class Car
        {
            public static CarEntity MapToCarEntity(DbCarEntity dbCarEntity)
            {
                if (dbCarEntity == null) return null;
                return new CarEntity
                (
                    Vin.FromString(dbCarEntity.Vin),
                    dbCarEntity.Brand,
                    dbCarEntity.Model,
                    dbCarEntity.Mileage,
                    dbCarEntity.PricePerDay,
                    dbCarEntity.Year
                );
            }
            public static DbCarEntity MapToDbCarEntity(CarEntity carEntity)
            {
                if (carEntity == null) return null;
                return new DbCarEntity()
                {
                    Vin = carEntity.Vin.Value,
                    Brand = carEntity.Brand,
                    Model = carEntity.Model,
                    Mileage = carEntity.Mileage,
                    PricePerDay = carEntity.PricePerDay,
                    Year = carEntity.Year
                };
            }
        }
    }
}
