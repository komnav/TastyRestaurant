using Application.Dtos;
using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(UserManager<User> _userManager, IOptions<JwtSettingOptions> jwtSettings)
     : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid username or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = CreateToken(user, roles);

            return Ok(token);
        }

        private string CreateToken(User user, IEnumerable<string> roles)
        {
            var _jwtSettings = jwtSettings.Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            foreach (var role in roles)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
