using Database.Context;
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

        public bool CreateAndSave(DbClientEntity client)
        {
            _DbContext.Clients.Add(client);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool UpdateAndSave(DbClientEntity client)
        {
            _DbContext.Clients.Update(client);
            return DatabaseUtils.CommitChanges(_DbContext) > 0;
        }

        public bool DeleteAndSave(Guid clientGuid)
        {
            var clientToDelete = Get(clientGuid);
            if (clientToDelete != null)
            {
                _DbContext.Clients.Remove(clientToDelete);
                return DatabaseUtils.CommitChanges(_DbContext) > 0;
            }
            return false;
        }
    }
}
