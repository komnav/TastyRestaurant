using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Restaurant.WebApi.Extensions;

public static class AuthorizationWithIdentity
{
    public static void AddAuthorizationWithIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.BearerScheme;
            options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
            options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
        }).AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorization();
    }
}
