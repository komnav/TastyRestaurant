using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;

namespace RestaurantLayer.Services
{
    public interface IUpdateRolesService
    {
        Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request);
    }
}
