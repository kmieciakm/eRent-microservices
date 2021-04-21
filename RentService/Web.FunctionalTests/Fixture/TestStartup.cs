using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Setup;

namespace Web.FunctionalTests.Fixture
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly);

            services
                .AddDbContext(options => options
                    .UseInMemoryDatabase("InMemoryTestRentDatabase")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                .AddSeedSettings(Configuration)
                .AddRepositories()
                .AddApplicationServices();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.SeedRentDatabase(serviceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
