using Database.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grpc.Helpers
{
    public static class StartupExtensions
    {
        public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<UserContext>();
            var dbSeed = new UserContextSeed(dbContext);

            dbSeed.Seed();

            return serviceProvider;
        }
    }
}
