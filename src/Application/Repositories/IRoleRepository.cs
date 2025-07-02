using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace RestaurantLayer.Repositories;

public interface IRoleRepository
{
    Task<int> UpdateAsync(string user, string role);
    Task<int> DeleteAsync(string userName, string roles);
    Task<int> AddRoleAsync(string role);

    Task<User?> GetRoleUserAsync(string userName);
    Task<List<User>> GetAllUserAsync(string userName);
}