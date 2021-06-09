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
using Domain.Ports.Infrastructure;
using MessageQueue;

namespace Web.Setup
{
    public static class ServiceManager
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<RentDbContext>(optionsAction, contextLifetime: ServiceLifetime.Singleton);
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
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IClientQuery, ClientAdapter>();
            services.AddSingleton<IClientCreate, ClientAdapter>();
            services.AddSingleton<IClientModify, ClientAdapter>();
            services.AddSingleton<IClientDelete, ClientAdapter>();

            services.AddSingleton<IRentRepository, RentRepository>();
            services.AddSingleton<IRentQuery, RentAdapter>();
            services.AddSingleton<IRentCreator, RentAdapter>();
            services.AddSingleton<IRentModify, RentAdapter>();
            services.AddSingleton<IRentCancel, RentAdapter>();

            services.AddSingleton<ICarRepository, CarRepository>();
            services.AddSingleton<ICarQuery, CarAdapter>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICarRentService, CarRentService>();
            services.AddSingleton<ICarService, CarService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IAutomaticAccountCreator, AccountCreator>();

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
