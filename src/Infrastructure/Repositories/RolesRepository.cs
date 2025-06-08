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

        public async Task<List<User?>> GetUserNameByRoleAsync(string role)
        {
            return await _applicationDbContext.Users
                .Where(u => u.Role == role).ToListAsync();
        }

        public async Task<List<User?>> GetRolesByUserNameAsync(string userName)
        {
            return await _applicationDbContext.Users.Where(x => x.UserName == userName).ToListAsync();
        }
    }
}