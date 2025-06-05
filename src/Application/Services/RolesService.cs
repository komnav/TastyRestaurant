using Application.Dtos;
using Application.Dtos.Role.Requests;
using RestaurantLayer.Dtos.Role.Responses;
using RestaurantLayer.Repositories;

namespace Application.Services
{
    public class RolesService(IRolesRepository rolesRepository) : IRolesService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;

        public async Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request)
        {
            var updateRoles = await _rolesRepository.UpdateAsync(request.UserName, request.Role);

            return new UpdateResponseModel(updateRoles);
        }

        public async Task<int> DeleteAsync(string userName, string roles)
        {
            return await _rolesRepository.DeleteAsync(userName, roles);
        }

        public async Task<GetRoleResponseModel> GetUserRoleByNameAsync(string role)
        {
            var getRole = await _rolesRepository.GetUserRoleByNameAsync(role);
            if (getRole == null)
                return null!;
            return new GetRoleResponseModel(getRole.Role);
        }

        public async Task<List<GetRoleResponseModel>> GetUsersAsync()
        {
            var getRole = await _rolesRepository.GetUsersAsync();
            return getRole.Select(role => new GetRoleResponseModel(
                role.Role
            )).ToList();
        }
    }
}