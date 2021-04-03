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
                    dbCarEntity.Year
                );
            }
        }
    }
}
