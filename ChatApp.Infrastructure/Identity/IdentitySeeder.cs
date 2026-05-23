using Microsoft.AspNetCore.Identity;

namespace ChatApp.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(
            RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager
                .RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(
                    new IdentityRole("Admin"));
            }

            if (!await roleManager
                .RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(
                    new IdentityRole("User"));
            }
        }
    }
}