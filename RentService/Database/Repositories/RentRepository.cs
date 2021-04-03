using Database.DatabaseContext;
using Database.Entities;
using Database.Helpers;
using Database.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
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
                .Include(rent => rent.Client)
                .FirstOrDefault(rent => rent.RentGuid.Equals(rentGuid));
        }

        public IEnumerable<DbRentEntity> GetByClient(Guid clientGuid)
        {
            return _DbContext.Rents
                .Where(rent => rent.ClientGuid.Equals(clientGuid));
        }

        public bool CreateAndSave(DbRentEntity rent)
        {
            _DbContext.Rents.Add(rent);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool UpdateAndSave(DbRentEntity rent)
        {
            _DbContext.Rents.Update(rent);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool DeleteAndSave(Guid rentGuid)
        {
            var rentToDelete = Get(rentGuid);
            if (rentToDelete != null)
            {
                _DbContext.Rents.Remove(rentToDelete);
                return DatabaseUtils.CommitChanges(_DbContext) > 0;
            }
            return false;
        }
    }
}
