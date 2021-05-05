using Database.Context;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
        {
            services
                .AddDbContext<UserContext>(options => options.UseInMemoryDatabase("InMemoryUserDatabase"))
                .AddIdentity<DbUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();

            return services;
        }

        public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<UserContext>();
            var dbSeed = new UserContextSeed(dbContext);

            dbSeed.Seed();

            return serviceProvider;
        }
    }
}
