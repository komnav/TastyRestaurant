using System.Text;
using Application.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Restaurant.WebApi.DI;

public static class JwtTokenServiceExtensions
{
    public static void AddJwtTokenService(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.Configure<JwtSettingOptions>(
            builder.Configuration.GetSection("Jwt"));

        var jwtSettings = config.GetSection("Jwt").Get<JwtSettingOptions>();

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ValidateIssuer = true,
                ValidateAudience = true
            };
        });
    }
}