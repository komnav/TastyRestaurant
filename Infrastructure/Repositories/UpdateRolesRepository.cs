
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UpdateRolesRepository(ApplicationDbContext applicationDbContext) : IUpdateRolesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
        public async Task<int> UpdateAsync(string userName, string roles)
        {
            return await _applicationDbContext.Users
                .Where(u => u.UserName == userName)
                .ExecuteUpdateAsync(x => x
                .SetProperty(r => r.Role, roles));
        }
    }
}
