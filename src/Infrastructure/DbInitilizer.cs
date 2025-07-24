using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbInitilizer(
        ApplicationDbContext applicationDbContext,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        private bool SupportMigration { get; set; } = true;

        public async Task Init()
        {
            if (SupportMigration)
            {
                await applicationDbContext.Database.MigrateAsync();
            }

            var findAdminRole = await roleManager.FindByNameAsync("SuperAdmin");
            if (findAdminRole == null)
            {
                var superAdminRole = new IdentityRole<int>("SuperAdmin");
                var adminRole = new IdentityRole<int>("admin");

                var resultSuperAdmin = await roleManager.CreateAsync(superAdminRole);
                var resultAdmin = await roleManager.CreateAsync(adminRole);

                if (!resultSuperAdmin.Succeeded && !resultAdmin.Succeeded)
                {
                    throw new Exception("Error creating role");
                }
            }

            var users = await userManager.GetUsersInRoleAsync("SuperAdmin");

            if (users.Count == 0)
            {
                var user = new User
                {
                    UserName = "SuperAdmin",
                    Email = "admin@example.com",
                };

                var result = await userManager.CreateAsync(user, "Admin1234$");
                if (!result.Succeeded)
                {
                    throw new Exception("Error creating user");
                }

                result = await userManager.AddToRoleAsync(user, "SuperAdmin");
                result = await userManager.AddToRoleAsync(user, "admin");

                if (!result.Succeeded)
                {
                    throw new Exception("Error assigning role to user");
                }
            }
        }
    }
}