using Application.Dtos;
using Application.Dtos.Role.Requests;
using RestaurantLayer.Dtos.Role.Responses;

namespace Application.Services
{
    public interface IRolesService
    {
        Task<CreateRolesResponseModel> CreateAsync(CreateRolesRequestModel requestModel);
        Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request);
    }
}