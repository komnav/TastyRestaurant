using Domain.Entities;

namespace RestaurantLayer.Repositories
{
    public interface IRolesRepository
    {
        Task<int> UpdateAsync(string userName, string roles);
        Task<int> DeleteAsync(string userName, string roles);
        Task<User?> GetUserRoleByNameAsync(string role);
        Task<List<User?>> GetUsersAsync();
    }
}