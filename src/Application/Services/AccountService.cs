using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using RestaurantLayer.Exceptions;
using RestaurantLayer.Repositories;


namespace Application.Services
{
    public class AccountService(IAccountRepository accountRepository, IOptions<JwtSettingOptions> jwtSettings)
        : IAccountService
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

        public string CreateToken(User? user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, user.Role),
            };

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

        public async Task<AuthResponse?> LoginAsync(LoginRequestModel request)
        {
            var getUser = await _accountRepository.GetAsync(request.UserName, request.Password);
            if (getUser == null)
            {
                return null;
            }

            var newToken = CreateToken(getUser);

            return new AuthResponse
            {
                Token = newToken
            };
        }
    }
}