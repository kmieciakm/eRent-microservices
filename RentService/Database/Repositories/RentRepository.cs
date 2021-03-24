using Database.DatabaseContext;
using Database.Entities;
using Database.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    class RentRepository : IRentRepository
    {
        private RentDbContext _DbContext { get; set; }
        public RentRepository(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public DbRentEntity Get(Guid rentGuid)
        {
            return _DbContext.Rents
                .FirstOrDefault(rent => rent.RentGuid.Equals(rentGuid));
        }

        public IEnumerable<DbRentEntity> GetByClient(Guid clientGuid)
        {
            return _DbContext.Rents
                .Where(rent => rent.ClientGuid.Equals(clientGuid));
        }

        public bool CreateAndSave(DbRentEntity rentEnt)
        {
            _DbContext.Rents.Add(rentEnt);
            return _DbContext.SaveChanges() >= 1;
        }

        public bool UpdateAndSave(DbRentEntity rentEnt)
        {
            _DbContext.Rents.Update(rentEnt);
            return _DbContext.SaveChanges() >= 1;
        }

        public bool DeleteAndSave(Guid rentGuid)
        {
            var rent = Get(rentGuid);
            if (rent != null)
            {
                _DbContext.Rents.Remove(rent);
                return _DbContext.SaveChanges() >= 1;
            }
            return false;
        }
    }
}
