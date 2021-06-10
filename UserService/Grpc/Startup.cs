using Database;
using Database.Context;
using Database.Models;
using Domain.Infrastructure;
using Domain.Models;
using Domain.Services;
using Domain.Services.Contracts;
using Grpc.Helpers;
using Grpc.Services;
using MessageQueue.Mailing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grpc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcReflection();

            // Database setup
            services
                .AddDbContext<UserContext>(options => options.UseInMemoryDatabase("InMemoryUserDatabase"))
                .AddIdentity<DbUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();

            // Repositories
            services.AddScoped<IUserRegistry, UserRegistry>();

            // Settings
            services.Configure<AuthenticationSettings>(Configuration.GetSection("AuthenticationSettings"));
            services.Configure<MailingSettings>(Configuration.GetSection("MailingSettings"));

            // Domain services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                serviceProvider.SeedDatabase();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcAuthenticationService>();

                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }
            });
        }
    }
}
