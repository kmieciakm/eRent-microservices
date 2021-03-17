using Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Repositories
{
    class RentRepository
    {
        private RentDbContext _DbContext { get; set; }
        public RentRepository(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
