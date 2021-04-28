using Database;
using Domain.Infrastructure;
using Domain.Models;
using Domain.Services;
using Domain.Services.Contracts;
using Grpc.FunctionalTests.Protos;
using Grpc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grpc.FunctionalTests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            // Database setup
            services
                .AddDbContext<UserContext>(options => options.UseInMemoryDatabase("TestUserDatabase"))
                .AddIdentity<DbUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();

            services.AddSingleton<IUserRegistry, UserRegistry>();

            // Domain services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddGrpcClient<Authentication.AuthenticationClient>(clientOptions =>
            {
                clientOptions.Address = new Uri("https://localhost:5001");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            serviceProvider
                .GetRequiredService<UserContext>()
                .Seed();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcAuthenticationService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
