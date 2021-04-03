using Database.DatabaseContext;
using Database.Entities;
using Database.Helpers;
using Database.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    class ClientRepository : IClientRepository
    {
        private RentDbContext _DbContext { get; set; }
        public ClientRepository(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public DbClientEntity Get(Guid clientGuid)
        {
            return _DbContext.Clients
                .FirstOrDefault(client => client.ClientGuid.Equals(clientGuid));
        }

        public bool CreateAndSave(DbClientEntity clientEnt)
        {
            _DbContext.Clients.Add(clientEnt);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool UpdateAndSave(DbClientEntity clientEnt)
        {
            _DbContext.Clients.Update(clientEnt);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool DeleteAndSave(Guid clientGuid)
        {
            DbClientEntity clientEntity = Get(clientGuid);
            if (clientEntity != null)
            {
                _DbContext.Clients.Remove(clientEntity);
                return DatabaseUtils.CommitChanges(_DbContext) > 0;
            }
            return false;
        }
    }
}
