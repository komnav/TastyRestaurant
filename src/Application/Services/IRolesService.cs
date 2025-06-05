using Application.Dtos;
using Application.Dtos.Role.Requests;
using RestaurantLayer.Dtos.Role.Responses;

namespace Application.Services
{
    public interface IRolesService
    {
        Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request);
        Task<int> DeleteAsync(string userName, string roles);
        Task<GetRoleResponseModel> GetUserRoleByNameAsync(string role);
        Task<List<GetRoleResponseModel>> GetUsersAsync();
    }
}