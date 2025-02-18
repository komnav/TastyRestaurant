using Infrastructure.Repositories;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;

namespace RestaurantLayer.Services
{
    public class UpdateRolesService(IUpdateRolesRepository updateRolesRepository) : IUpdateRolesService
    {
        private readonly IUpdateRolesRepository _updateRolesRepository = updateRolesRepository;
        public async Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request)
        {
            var updateRoles = await _updateRolesRepository.UpdateAsync(request.UserName, request.Role);

            return new UpdateResponseModel(updateRoles);
        }
    }
}
