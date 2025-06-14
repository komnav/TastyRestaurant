using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.WebApi.Extensions;

public static class AuthorizationWithIdentity
{
    public static void AddAuthorizationWithIdentity(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddCookie(IdentityConstants.ApplicationScheme);

        
        builder.Services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();
        
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        });
        
    }
}