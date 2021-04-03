using Database.DatabaseContext;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            SeedClientsFromCSV();
            SeedRents();
            DetachAllEntries();
        }

        private void SeedClientsFromCSV()
        {
            var clientsCSV = Path.Combine(
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(RentDbContextSeed)).Location),
                "TestData" + Path.DirectorySeparatorChar + "Clients.csv");
            _DbContext.Clients.AddRange(GetClientsFromCSV(clientsCSV));
            _DbContext.SaveChangesAsync();
        }

        // TODO: Parse Rents data from CSV
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

        private void DetachAllEntries()
        {
            _DbContext.ChangeTracker.Clear();
        }

        private static List<DbClientEntity> GetClientsFromCSV(string csvClientFilePath)
        {
            if (!File.Exists(csvClientFilePath)) return new List<DbClientEntity>();

            return File.ReadAllLines(csvClientFilePath)
                .Select(line => {
                    var clientsFields = line.Split(",");
                    return new DbClientEntity()
                    {
                        ClientGuid = Guid.Parse(clientsFields[0]),
                        Firstname = clientsFields[1],
                        Lastname = clientsFields[2],
                        Email = clientsFields[3]
                    };
                })
                .ToList();
        }
    }
}
