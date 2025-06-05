using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RestaurantLayer.Repositories;

namespace Infrastructure.Repositories
{
    public class RolesRepository(ApplicationDbContext applicationDbContext) : IRolesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<int> UpdateAsync(string userName, string roles)
        {
            return await _applicationDbContext.Users
                .Where(u => u.UserName == userName)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(r => r.Role, roles));
        }

        public async Task<int> DeleteAsync(string userName, string roles)
        {
            return await _applicationDbContext.Users
                .Where(u => u.UserName == userName)
                .ExecuteDeleteAsync();
        }

        public async Task<User?> GetUserRoleByNameAsync(string role)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u != null && u.Role == role);
        }

        public async Task<List<User?>> GetUsersAsync()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }
    }
}