using Domain.Entities;
using RestaurantLayer.Dtos.Account.Requests;
using RestaurantLayer.Dtos.Account.Responses;
using System.ComponentModel.DataAnnotations;

namespace RestaurantLayer.Services
{
    public interface IAccountService
    {
        Task<AuthResponse> CreateAsync(RegisterUserRequest request);
        Task<GetUserResponseModel> GetAsync(string userName, string password);
        string CreateToken(User request);
    }
}
