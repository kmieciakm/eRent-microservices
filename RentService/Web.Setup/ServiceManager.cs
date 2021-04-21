using Database.Adapters;
using Database.Context;
using Database.Seed;
using Database.Repositories;
using Database.Repositories.Contracts;
using Domain.Ports.Infrastructure.Car;
using Domain.Ports.Infrastructure.Client;
using Domain.Ports.Infrastructure.Rent;
using Domain.Ports.Presenters;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Setup
{
    public static class ServiceManager
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<RentDbContext>(optionsAction);
            return services;
        }

        public static IServiceCollection AddSeedSettings(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection settingsSection = configuration.GetSection("SeedSettings");
            var settings = new RentDbSeedSettings();
            settingsSection.Bind(settings);

            services.AddSingleton<ISeedSettings>(_ => settings);
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientQuery, ClientAdapter>();
            services.AddScoped<IClientCreate, ClientAdapter>();
            services.AddScoped<IClientModify, ClientAdapter>();
            services.AddScoped<IClientDelete, ClientAdapter>();

            services.AddScoped<IRentRepository, RentRepository>();
            services.AddScoped<IRentQuery, RentAdapter>();
            services.AddScoped<IRentCreator, RentAdapter>();
            services.AddScoped<IRentModify, RentAdapter>();
            services.AddScoped<IRentCancel, RentAdapter>();

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarQuery, CarAdapter>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICarRentService, CarRentService>();
            services.AddScoped<ICarService, CarService>();

            return services;
        }

        public static IApplicationBuilder SeedRentDatabase(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<RentDbContext>();
            var seedSettings = serviceProvider.GetRequiredService<ISeedSettings>();
            var dbSeed = new RentDbSeed(dbContext, seedSettings);
            dbSeed.SeedData();

            return app;
        }
    }
}
