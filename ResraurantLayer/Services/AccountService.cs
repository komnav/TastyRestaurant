using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Account.Requests;
using RestaurantLayer.Dtos.Account.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RestaurantLayer.Services
{
    public class AccountService(IAccountRepository accountRepository, IOptions<JwtSettingOptions> jwtSettings) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly JwtSettingOptions _jwtSettings = jwtSettings.Value;

        public async Task<AuthResponse> CreateAsync(RegisterUserRequest request)
        {
            var contact = new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password,
                Role = UserRoles.Customer,
                Contact = contact
            };

            await _accountRepository.CreateAsync(contact, user);


            var token = CreateToken(user);

            return new AuthResponse { Token = token };

        }

        public string CreateToken(User request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
        {
            new(ClaimTypes.Role, request.Role),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<GetUserResponseModel> GetAsync(string userName, string password)
        {
            var user = await _accountRepository.GetAsync(userName, password);

            var token = CreateToken(user);
            return new GetUserResponseModel
            {
                Token = token
            };
        }
    }
}
