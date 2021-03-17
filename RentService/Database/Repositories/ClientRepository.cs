using Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Repositories
{
    class ClientRepository
    {
        private RentDbContext _DbContext { get; set; }
        public ClientRepository(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
