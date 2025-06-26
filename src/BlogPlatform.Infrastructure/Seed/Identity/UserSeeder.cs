using BlogPlatform.Application.Enum;
using BlogPlatform.Domain.ApplicationUserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPlatform.Infrastructure.Seed.Identity
{
    public static class UserSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            const string adminEmail = "admin@blogplatform.com";
            const string adminPassword = "Admin@123";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin != null)
                return;

            if (!await roleManager.RoleExistsAsync(RolesEnum.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(RolesEnum.Admin.ToString()));
            }

            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, RolesEnum.Admin.ToString());

        }

    }

}

