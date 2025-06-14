using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebApi.Extensions;

public static class AuthorizationWithIdentity
{
    public static void AddAuthorizationWithIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}