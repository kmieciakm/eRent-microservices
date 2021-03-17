using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DatabaseContext
{
    class DatabaseContextSeed
    {
        private RentDbContext _DbContext { get; set; }
        public DatabaseContextSeed(RentDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void SeedData()
        {
            ClearDatabase();
            SeedClients();
            SeedRents();
            _DbContext.SaveChanges();
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
                    RentedVehicleVin = "12345678901234",
                    ClientGuid = _DbContext.Clients.FirstOrDefault().ClientGuid
                }
            );
        }

        private void ClearDatabase()
        {
            List<Task> clearSetTasks = new List<Task>() {
                ClearSet(_DbContext.Clients),
                ClearSet(_DbContext.Rents),
            };
            Task.WhenAll(clearSetTasks);
        }

        private async Task ClearSet<T>(DbSet<T> dbSet) where T : class
        {
            await dbSet.ForEachAsync(entity => dbSet.Remove(entity));
        }
    }
}
