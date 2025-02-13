using Domain.Entities;
using RestaurantLayer.Dtos.Account.Requests;
using RestaurantLayer.Dtos.Account.Responses;

namespace RestaurantLayer.Services
{
    public interface IAccountService
    {
        Task<AuthResponse> CreateAsync(RegisterUserRequest request);
        //Task<GetUserResponseModel?> GetAsync(string userName);
        string CreateToken(User request);
    }
}
