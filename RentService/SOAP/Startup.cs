using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SOAP.Services;
using SoapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Web.Setup;

namespace SOAP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSoapExceptionTransformer((ex) => ex.Message);
            services.AddSoapCore();

            services
                .AddDbContext(options => options
                    .UseInMemoryDatabase("InMemoryRentDatabase")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                .AddSeedSettings(Configuration)
                .AddRepositories()
                .AddApplicationServices();

            services.TryAddSingleton<PingSOAPService>();
            services.TryAddSingleton<RentSOAPService>();

            services.AddMvc();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.SeedRentDatabase(serviceProvider);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.UseSoapEndpoint<PingSOAPService>("/PingService.asmx", new BasicHttpBinding());
                endpoints.UseSoapEndpoint<RentSOAPService>("/RentService.asmx", new BasicHttpBinding());
            });
        }
    }
}
