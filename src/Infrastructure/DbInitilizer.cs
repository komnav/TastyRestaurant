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

            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                var role = new IdentityRole<int>("admin");

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception("Error creating role");
                }
            }

            var users = await userManager.GetUsersInRoleAsync("admin");

            if (users.Count == 0)
            {
                var user = new User
                {
                    UserName = "superAdmin",
                    Email = "admin@example.com",
                };

                var result = await userManager.CreateAsync(user, "Admin1234$");
                if (!result.Succeeded)
                {
                    throw new Exception("Error creating user");
                }

                result = await userManager.AddToRoleAsync(user, "admin");

                if (!result.Succeeded)
                {
                    throw new Exception("Error assigning role to user");
                }
            }
        }
    }
}