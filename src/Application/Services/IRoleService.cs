using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;

namespace RestaurantLayer.Services;

public interface IRoleService
{
    Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request);
    Task<int> DeleteAsync(string userName, string roles);
    Task<int> AddRoleAsync(string role);
}