using Database.DatabaseContext;
using Database.Entities;
using Database.IntegrationTests.Fixture.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Database.IntegrationTests.TestFixture
{
    class DatabaseSeed
    {
        private RentDbContext _DbContext { get; set; }
        private ISeedSettings _SeedSettings { get; set; }

        public DatabaseSeed(RentDbContext dbContext, ISeedSettings seedSettings)
        {
            _DbContext = dbContext;
            _SeedSettings = seedSettings;
        }

        public void SeedData()
        {
            SeedClientsFromCSV();
            SeedRentsFromCSV();
            DetachAllEntries();
        }

        private void SeedClientsFromCSV()
        {
            var clientsCSV = Path.Combine(
                Directory.GetCurrentDirectory(),
                _SeedSettings.ClientDataRelativePath);

            _DbContext.Clients.AddRange(GetClientsFromCSV(clientsCSV));
            _DbContext.SaveChangesAsync();
        }

        private void SeedRentsFromCSV()
        {
            var rentsCSV = Path.Combine(
                Directory.GetCurrentDirectory(),
                _SeedSettings.RentsDataRelativePath);

            _DbContext.Rents.AddRange(GetRentsFromCSV(rentsCSV));
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

        private static List<DbRentEntity> GetRentsFromCSV(string csvRentFilePath)
        {
            if (!File.Exists(csvRentFilePath)) return new List<DbRentEntity>();

            return File.ReadAllLines(csvRentFilePath)
                .Select(line => {
                    var rentsFields = line.Split(",");
                    return new DbRentEntity()
                    {
                        RentGuid = Guid.Parse(rentsFields[0]),
                        RentalDate = DateTime.Parse(rentsFields[1]),
                        EndRentalDate = DateTime.Parse(rentsFields[2]),
                        TotalRentPrice = decimal.Parse(rentsFields[3]),
                        RentedVehicleVin = rentsFields[4],
                        ClientGuid = Guid.Parse(rentsFields[5]),
                    };
                })
                .ToList();
        }
    }
}
