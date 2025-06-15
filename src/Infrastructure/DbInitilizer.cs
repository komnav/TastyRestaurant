using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbInitilizer(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
    {
        private bool SupportMigration { get; set; } = true;

        public async Task Init()
        {
            if (SupportMigration)
            {
                await applicationDbContext.Database.MigrateAsync();
            }

            if (!applicationDbContext.Users.Any(s => s.Role == UserRoles.SuperAdmin))
            {
                var user = new User
                {
                    UserName = "superAdmin",
                    Email = "admin@example.com",
                    Role = UserRoles.SuperAdmin,
                    PasswordHash =  new PasswordHasher<User>().HashPassword(null!, "1234String$")
                };

                await applicationDbContext.Users.AddAsync(user);
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}