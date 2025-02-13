using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using ResrautantLayer.Exceptions;
using RestaurantLayer.Dtos.Account.Requests;
using RestaurantLayer.Dtos.Account.Responses;
using RestaurantLayer.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RestaurantLayer.Services
{
    public class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        private const string TokenSecret = "TheFirstTestInJwtTokenTheFirstTestInJwtTokenTheFirstTestInJwtTokenTheFirstTestInJwtToken";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

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
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
        {
            new(ClaimTypes.Role, request.Role),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "https://id.nickchapsas.com",
                Audience = "https://movies.nickchapsas.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //public async Task<GetUserResponseModel?> GetAsync(string userName)
        //{
        //    var user = await _accountRepository.GetAsync(userName);

        //    var token = CreateToken(user);
        //    return new GetUserResponseModel
        //    {
        //        Token = token
        //    };
        //}
    }
}
