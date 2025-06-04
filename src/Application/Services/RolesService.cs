using Application.Dtos;
using Application.Dtos.Role.Requests;
using Infrastructure.Repositories;
using RestaurantLayer.Dtos.Role.Responses;

namespace Application.Services
{
    public class RolesService(IUpdateRolesRepository updateRolesRepository) : IRolesService
    {
        private readonly IUpdateRolesRepository _updateRolesRepository = updateRolesRepository;

        public Task<CreateRolesResponseModel> CreateAsync(CreateRolesRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request)
        {
            var updateRoles = await _updateRolesRepository.UpdateAsync(request.UserName, request.Role);

            return new UpdateResponseModel(updateRoles);
        }
    }
}