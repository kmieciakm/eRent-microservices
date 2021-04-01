using Database.DatabaseContext;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.IntegrationTests.TestFixture
{
    class RentDbContextSeed
    {
        private RentDbContext _DbContext { get; set; }
        public RentDbContextSeed(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void SeedData()
        {
            SeedClients();
            SeedRents();
        }

        private void SeedClients()
        {
            _DbContext.Clients.Add(
                new DbClientEntity()
                {
                    ClientGuid = Guid.NewGuid(),
                    Firstname = "Jan",
                    Lastname = "Kowalski",
                    Email = "jan.kow@wp.pl"
                }
            );
            _DbContext.SaveChangesAsync();
        }

        private void SeedRents()
        {
            _DbContext.Rents.Add(
                new DbRentEntity()
                {
                    RentGuid = Guid.NewGuid(),
                    RentalDate = DateTime.Now,
                    EndRentalDate = DateTime.Now.AddDays(7),
                    TotalRentPrice = 200m,
                    RentedVehicleVin = "12345678901234567",
                    ClientGuid = _DbContext.Clients.FirstOrDefault().ClientGuid
                }
            );
            _DbContext.SaveChangesAsync();
        }
    }
}
