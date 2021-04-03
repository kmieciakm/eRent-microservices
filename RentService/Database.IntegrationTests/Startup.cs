using Database.Adapters;
using Database.DatabaseContext;
using Database.IntegrationTests.Fixture;
using Database.IntegrationTests.Fixture.Settings;
using Database.IntegrationTests.TestFixture;
using Database.Repositories;
using Database.Repositories.Contracts;
using Domain.Ports.Infrastructure.Client;
using Domain.Ports.Infrastructure.Rent;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISeedSettings, TestSettings>();

            // Database context
            services.AddScoped(provider => SetupDbContext(provider));

            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRentRepository, RentRepository>();

            // Database use cases
            services.AddScoped<IClientQuery, ClientAdapter>();
            services.AddScoped<IClientCreate, ClientAdapter>();
            services.AddScoped<IClientModify, ClientAdapter>();
            services.AddScoped<IClientDelete, ClientAdapter>();
            services.AddScoped<IRentQuery, RentAdapter>();
            services.AddScoped<IRentCreator, RentAdapter>();
            services.AddScoped<IRentModify, RentAdapter>();
            services.AddScoped<IRentCancel, RentAdapter>();
        }

        private static RentDbContext SetupDbContext(IServiceProvider serviceProvider)
        {
            var settings = serviceProvider.GetRequiredService<ISeedSettings>();
            var dbContext = new InMemoryDatabase().CreateDbContext();
            var dbContextSeed = new DatabaseSeed(dbContext, settings);
            dbContextSeed.SeedData();
            return dbContext;
        }
    }
}
