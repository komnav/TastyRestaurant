using Domain.Entities;

namespace RestaurantLayer.Repositories
{
    public interface IRolesRepository
    {
        Task<int> UpdateAsync(string userName, string roles);
        Task<int> DeleteAsync(string userName, string roles);
        Task<List<User?>> GetUserNameByRoleAsync(string role);
        Task<List<User?>> GetRolesByUserNameAsync(string userName);
    }
}