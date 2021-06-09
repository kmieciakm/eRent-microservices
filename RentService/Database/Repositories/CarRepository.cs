using Database.Context;
using Database.Entities;
using Database.Helpers;
using Database.Repositories.Contracts;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    class CarRepository : ICarRepository
    {
        private RentDbContext _DbContext { get; set; }
        public CarRepository(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public DbCarEntity Get(Vin vin)
        {
            return _DbContext.Cars
                .FirstOrDefault(car => car.Vin == vin.Value);
        }

        public bool Exist(Vin vin)
        {
            return _DbContext.Cars
               .Any(car => car.Vin == vin.Value);
        }

        public bool CreateAndSave(DbCarEntity car)
        {
            _DbContext.Cars.Add(car);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool UpdateAndSave(DbCarEntity car)
        {
            _DbContext.Cars.Update(car);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool DeleteAndSave(Vin vin)
        {
            var carToDelete = Get(vin);
            if (carToDelete != null)
            {
                _DbContext.Cars.Remove(carToDelete);
                return DatabaseUtils.CommitChanges(_DbContext) > 0;
            }
            return false;
        }
    }
}
