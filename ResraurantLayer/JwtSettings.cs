using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestaurantLayer.Dtos;

namespace RestaurantLayer
{
    public class JwtSettings : IConfigureOptions<JwtSettingOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtSettingOptions options)
        {
            var settings = _configuration.GetRequiredSection("Jwt").Get<JwtSettingOptions>();
            if (settings != null)
            {
                options.Key = settings.Key;
                options.TokenLifetime = settings.TokenLifetime;
                options.Issuer = settings.Issuer;
                options.Audience = settings.Audience;
            }
        }

    }
}
