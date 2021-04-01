using Database.DatabaseContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace Database.IntegrationTests.TestFixture
{
    class InMemoryRentDatabase : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<RentDbContext> CreateOptions => new DbContextOptionsBuilder<RentDbContext>()
            .EnableSensitiveDataLogging()
            .EnableServiceProviderCaching(false)
            .UseSqlite(_connection).Options;

        public RentDbContext CreateDbContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=file::memory:");
                _connection.Open();

                var options = CreateOptions;
                using (var context = new RentDbContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            var dbContext = new RentDbContext(CreateOptions);
            return dbContext;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
