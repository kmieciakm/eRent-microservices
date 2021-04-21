using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.TryAddSingleton<PingService>();

            services.AddMvc();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.UseSoapEndpoint<PingService>("/PingService.asmx", new BasicHttpBinding());
            });
        }
    }
}
