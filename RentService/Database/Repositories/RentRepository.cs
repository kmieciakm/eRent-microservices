using Database.DatabaseContext;
using Database.Repositories.Contracts;
using System;
using System.Collections.Generic;
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
    }
}
