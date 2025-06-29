using System.Text;
using Application.Dtos;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Restaurant.WebApi.Extensions;

public static class AuthorizationWithIdentity
{
    public static void AddAuthorizationWithIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(op =>
            {
                op.DefaultScheme = IdentityConstants.ApplicationScheme;
                op.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();
    }
}