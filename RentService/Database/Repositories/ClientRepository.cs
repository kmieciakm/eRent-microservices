using Database.DatabaseContext;
using Database.Entities;
using Database.Repositories.Contracts;
using System;
using System.Collections.Generic;
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
    }
}
