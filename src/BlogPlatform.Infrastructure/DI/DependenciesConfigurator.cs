using BlogPlatform.Application.Common;
using BlogPlatform.Infrastructure.Interface.Auth;
using BlogPlatform.Infrastructure.Interface;
using BlogPlatform.Infrastructure.Seed.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogPlatform.Infrastructure.DI
{
    public static class DependenciesConfigurator
    {
        public static void ConfigureAppLogging(this IApplicationBuilder app, IConfiguration configuration)
        {
        }

        public static void ConfigureInfrastructure(this IApplicationBuilder app)
        {
        }


        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            await RoleSeeder.SeedRolesAsync(services);
        }
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBaseServices(configuration);
            services.AddBuildingBlocks(configuration);
            services.AddInfrastructureCore(configuration);
            services.AddLoggingServices(configuration);
            services.AddCacheServices(configuration);
            services.AddEventBusServices(configuration);
            services.AddFileStorageServices(configuration);
            services.AddLocalizationServices(configuration);
            services.AddUtilityServices(configuration);
            services.AddNotificationServices(configuration);
            services.AddJwtTokenService(configuration);
            services.AddEventBusAndHandlers(configuration);
            services.AddSignalRServices(configuration);
            services.AddHangFireJobs(configuration);
            services.AddMediatR(configuration);
            services.AddFluentValidation();
        }
        private static void AddDataBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogPlatformContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
        }
        private static void AddJwtTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,
                             ValidIssuer = configuration["Jwt:Issuer"],
                             ValidAudience = configuration["Jwt:Audience"],
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                         };
                     });
        }

        private static void AddFluentValidation(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddValidatorsFromAssemblies(assemblies);
        }

        private static void AddMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                             cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

        }
        private static void AddBuildingBlocks(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddInfrastructureCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        }

        private static void AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddEventBusServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddFileStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddLocalizationServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddUtilityServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
        {
        }



        private static void AddEventBusAndHandlers(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddSignalRServices(this IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddHangFireJobs(this IServiceCollection services, IConfiguration configuration)
        {
        }


        private static void AddIdentityServices(this IServiceCollection services)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<BlogPlatformContext>()
            //    .AddDefaultTokenProviders();
        }
    }
}
