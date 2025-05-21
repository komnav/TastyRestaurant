using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;

namespace Application.Services
{
    public interface IAccountService
    {
        Task<AuthResponse> CreateAsync(RegisterUserRequest request);
        Task<GetUserResponseModel> GetAsync(string userName, string password);
        string CreateToken(User request);
    }
}
