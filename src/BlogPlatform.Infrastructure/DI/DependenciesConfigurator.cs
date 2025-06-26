using BlogPlatform.API.Options;
using BlogPlatform.Application.Common;

using BlogPlatform.Application.Interfaces.FileStorge;
using BlogPlatform.Application.Interfaces.Repo;

using BlogPlatform.Infrastructure.FileStorge;
using BlogPlatform.Infrastructure.Interface;
using BlogPlatform.Infrastructure.Interface.Auth;
using BlogPlatform.Infrastructure.Repositories;
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
        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            await RoleSeeder.SeedRolesAsync(services);
            await UserSeeder.SeedAdminUserAsync(services);
        }
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBaseServices(configuration);
            services.AddOptions(configuration);
            services.AddBuildingBlocks(configuration);
            services.AddInfrastructureCore(configuration);
            services.AddLoggingServices(configuration);
            services.AddCacheServices(configuration);
            services.AddEventBusServices(configuration);
            services.AddFileStorageServices(configuration);
            services.AddLocalizationServices(configuration);
            services.AddUtilityServices(configuration);
            services.AddNotificationServices(configuration);
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
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileStorageService, FileStorageService>();

        }
        private static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(options => configuration.GetSection("Jwt").Bind(options));

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
