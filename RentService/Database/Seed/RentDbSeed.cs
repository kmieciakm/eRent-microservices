using Database.Context;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Database.Seed
{
    class RentDbSeed
    {
        private RentDbContext _DbContext { get; set; }
        private ISeedSettings _SeedSettings { get; set; }

        public RentDbSeed(RentDbContext dbContext, ISeedSettings seedSettings)
        {
            _DbContext = dbContext;
            _SeedSettings = seedSettings;
        }

        public void SeedData()
        {
            SeedClientsFromCSV();
            SeedCarsFromCSV();
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

        private void SeedCarsFromCSV()
        {
            var carsCSV = Path.Combine(
                Directory.GetCurrentDirectory(),
                _SeedSettings.CarsDataRelativePath);

            _DbContext.Cars.AddRange(GetCarsFromCSV(carsCSV));
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
                .Skip(1) // Skip header line
                .Select(line =>
                {
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

        private static List<DbCarEntity> GetCarsFromCSV(string csvCarsFilePath)
        {
            if (!File.Exists(csvCarsFilePath)) return new List<DbCarEntity>();

            return File.ReadAllLines(csvCarsFilePath)
                .Skip(1) // Skip header line
                .Select(line =>
                {
                    var carsFields = line.Split(",");
                    return new DbCarEntity()
                    {
                        Vin = carsFields[0],
                        Brand = carsFields[1],
                        Model = carsFields[2],
                        Mileage = long.Parse(carsFields[3]),
                        PricePerDay = decimal.Parse(carsFields[4]),
                        Year = int.Parse(carsFields[5])
                    };
                })
                .ToList();
        }

        private static List<DbRentEntity> GetRentsFromCSV(string csvRentFilePath)
        {
            if (!File.Exists(csvRentFilePath)) return new List<DbRentEntity>();

            return File.ReadAllLines(csvRentFilePath)
                .Skip(1) // Skip header line
                .Select(line =>
                {
                    var rentsFields = line.Split(",");
                    return new DbRentEntity()
                    {
                        RentGuid = Guid.Parse(rentsFields[0]),
                        RentalDate = DateTime.ParseExact(rentsFields[1], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        EndRentalDate = DateTime.ParseExact(rentsFields[2], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        TotalRentPrice = decimal.Parse(rentsFields[3]),
                        RentedVehicleVin = rentsFields[4],
                        ClientGuid = Guid.Parse(rentsFields[5])
                    };
                })
                .ToList();
        }
    }
}
