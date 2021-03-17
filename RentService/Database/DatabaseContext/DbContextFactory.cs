using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Database.DatabaseContext
{
    static class DbContextFactory
    {
        public static RentDbContext CreateInMemoryRentDbContext()
        {
            var options = new DbContextOptionsBuilder<RentDbContext>()
                .UseInMemoryDatabase("RentDb")
                .Options;

            var database = new RentDbContext(options);
            database.Database.EnsureCreated();

            var rentDbSeed = new RentDbContextSeed(database);
            rentDbSeed.SeedData();

            return database;
        }
    }
}
