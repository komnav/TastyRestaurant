using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace Restaurant.WebApi
{
    public class TokenGenerator
    {
        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = "TheFirstTestInJwtTokenTheFirstTestInJwtTokenTheFirstTestInJwtTokenTheFirstTestInJwtToken"u8.ToArray();


            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, email),
                new(JwtRegisteredClaimNames.Email, email),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "https://www.tripadvisor.com/Restaurant_Review-g811256-d26430302-Reviews-Mevlana-Khujand_Sughd_Province.html",
                Audience = "https://www.tripadvisor.com/Restaurant_Review-g811256-d26430302-Reviews-Mevlana-Khujand_Sughd_Province.html",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
