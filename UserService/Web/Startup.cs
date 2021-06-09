using Database;
using Domain.Infrastructure;
using Domain.Models;
using Domain.Services;
using Domain.Services.Contracts;
using MessageQueue;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web
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
            services.AddControllers();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication Service", Version = "v1" });
            });

            // Database setup
            services.AddInMemoryDatabase();

            // Repositories
            services.AddScoped<IUserRegistry, UserRegistry>();

            // Settings
            services.Configure<AuthenticationSettings>(Configuration.GetSection("AuthenticationSettings"));
            services.Configure<MailingSettings>(Configuration.GetSection("MailingSettings"));

            // Domain services
            services.AddScoped<ITokenService, TokenService>();
            // services.AddScoped<IMailSender, ConsoleMailSender>();
            services.AddScoped<IMailSender, MailingMQ>();
            services.AddScoped<IServicesManger, AccountMQ>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                serviceProvider.SeedDatabase();

                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication Service v1");
                    c.RoutePrefix = "docs";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IServiceCollection AddTokenAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection("AuthenticationSettings");
            var settings = settingsSection.Get<AuthenticationSettings>();
            var key = Encoding.ASCII.GetBytes(settings.Secret);
         
            services
                .Configure<AuthenticationSettings>(settingsSection)
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(authOptions =>
                {
                    authOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }
    }
}
