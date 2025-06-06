﻿using Domain.Entities;
using Domain.Enums;
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
                    Password = "12345678",
                    Role = UserRoles.SuperAdmin
                };

                applicationDbContext.Users.Add(user);
                applicationDbContext.SaveChanges();
            }
        }
    }
}
