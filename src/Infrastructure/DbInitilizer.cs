using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbInitilizer(ApplicationDbContext applicationDbContext)
    {
        public bool SupportMigration { get; set; } = true;

        public void Init()
        {
            if (SupportMigration)
            {
                applicationDbContext.Database.Migrate();
            }

            if (!applicationDbContext.Users.Any(s => s.Role == UserRoles.SuperAdmin))
            {
                var user = new User
                {
                    UserName = "superAdmin",
                    Email = "admin@example.com",
                    Role = UserRoles.SuperAdmin,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null!, "1234")
                };

                applicationDbContext.Users.Add(user);
                applicationDbContext.SaveChanges();
            }
        }
    }
}