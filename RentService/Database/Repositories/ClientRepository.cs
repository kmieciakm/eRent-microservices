using Database.DatabaseContext;
using Database.Entities;
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

        public bool CreateAndSave(DbClientEntity clientEnt)
        {
            _DbContext.Clients.Add(clientEnt);
            return _DbContext.SaveChanges() >= 1;
        }

        public bool DeleteAndSave(Guid clientGuid)
        {
            DbClientEntity clientEntity = GetClient(clientGuid);
            if (clientEntity != null)
            {
                _DbContext.Clients.Remove(clientEntity);
                return _DbContext.SaveChanges() >= 1;
            }
            return false;
        }

        public bool UpdateAndSave(DbClientEntity clientEnt)
        {
            _DbContext.Clients.Update(clientEnt);
            return _DbContext.SaveChanges() >= 1;
        }

        public DbClientEntity GetClient(Guid clientGuid)
        {
            return _DbContext.Clients
                .FirstOrDefault(client => client.ClientGuid.Equals(clientGuid));
        }
    }
}
