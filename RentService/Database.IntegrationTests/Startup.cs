using Database.Adapters;
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
            // Database context
            services.AddScoped(provider => new InMemoryRentDatabase().CreateDbContext());

            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRentRepository, RentRepository>();

            // Database use cases
            services.AddScoped<IClientQuery, ClientAdapter>();
            services.AddScoped<IClientCreate, ClientAdapter>();
            services.AddScoped<IClientModify, ClientAdapter>();
            services.AddScoped<IClientDelete, ClientAdapter>();
            services.AddScoped<IRentQuery, RentAdapter>();
            services.AddScoped<IRentCreate, RentAdapter>();
            services.AddScoped<IRentModify, RentAdapter>();
            services.AddScoped<IRentCancel, RentAdapter>();
        }
    }
}
