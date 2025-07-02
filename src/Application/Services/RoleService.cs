using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.Role.Requests;
using RestaurantLayer.Exceptions;
using RestaurantLayer.Repositories;

namespace RestaurantLayer.Services;

public class RoleService(IRoleRepository rolesRepository) : IRoleService
{
    private readonly IRoleRepository _rolesRepository = rolesRepository;

    public async Task<UpdateResponseModel> UpdateAsync(UpdateRolesRequestModel request)
    {
        var updateRoles = await _rolesRepository.AddToRoleAsync(request.UserName, request.Role);

        return new UpdateResponseModel(updateRoles);
    }

    public async Task<int> DeleteAsync(string userName, string roles)
    {
        return await _rolesRepository.DeleteAsync(userName, roles);
    }

    public async Task<int> AddRoleAsync(string role)
    {
        var tryToAdd = await _rolesRepository.AddRoleAsync(role);

        if (tryToAdd <= 0) 
            throw new ResourceAlreadyExistException("Role already exists");

        return 1;
    }
}