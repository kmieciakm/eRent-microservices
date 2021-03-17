using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.DatabaseContext
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
            _DbContext.SaveChangesAsync();
        }

        private void SeedClients()
        {
            _DbContext.Clients.Add(
                new ClientEnt()
                {
                    ClientGuid = Guid.NewGuid(),
                    Firstname = "Jan",
                    Lastname = "Kowalski",
                    Email = "jan.kow@wp.pl"
                }
            );
        }

        private void SeedRents()
        {
            _DbContext.Rents.Add(
                new RentEnt()
                {
                    RentGuid = Guid.NewGuid(),
                    RentalDate = DateTime.Now,
                    EndRentalDate = DateTime.Now.AddDays(7),
                    TotalRentPrice = 200m,
                    RentedVehicleVin = "12345678901234567",
                    ClientGuid = _DbContext.Clients.FirstOrDefault().ClientGuid
                }
            );
        }
    }
}
