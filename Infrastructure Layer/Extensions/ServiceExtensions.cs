using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrainingApp.Application_Layer.Services;
using TrainingApp.Domain_Layer.Interfaces;
using TrainingApp.Infrastructure_Layer.Repositories;

namespace TrainingApp.Infrastructure_Layer.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Add application services
            services.AddControllers();
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<WeatherForecastService>();

            // Add more services as needed
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Add Swagger services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }

        public static void AddIdentityAndJwtAuthentication(this IServiceCollection services, string key, string connectionString) {
            // Add Identity services with SQLite
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString)); // SQLite database

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // JWT Authentication
            var keyBytes = Encoding.ASCII.GetBytes(key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "yourdomain.com",
                    ValidAudience = "yourdomain.com",
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });
        }
    }
}
