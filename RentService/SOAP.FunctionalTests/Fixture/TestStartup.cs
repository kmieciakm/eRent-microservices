using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SOAP;
using SOAP.Services;
using SoapCore;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Web.Setup;

namespace SOAP.FunctionalTests.Fixture
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSoapCore();

            services
                .AddDbContext(options => options
                    .UseInMemoryDatabase("InMemoryTestRentDatabase")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                .AddSeedSettings(Configuration)
                .AddRepositories()
                .AddApplicationServices();

            services.TryAddSingleton<PingSOAPService>();
            services.TryAddSingleton<RentSOAPService>();

            services.AddMvc();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.SeedRentDatabase(serviceProvider);

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.UseSoapEndpoint<PingSOAPService>("/PingService.asmx", new BasicHttpBinding());
                endpoints.UseSoapEndpoint<RentSOAPService>("/RentService.asmx", new BasicHttpBinding());
            });
        }
    }
}
