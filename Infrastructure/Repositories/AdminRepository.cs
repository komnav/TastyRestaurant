using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdminRepository(ApplicationDbContext dbContext) : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Admin admin)
        {
            await _dbContext.Admins.AddAsync(admin);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Admins
                 .Where(x => x.Id.Equals(id))
                 .ExecuteDeleteAsync();
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await _dbContext.Admins.ToListAsync();
        }

        public async Task<Admin?> GetAsync(int id)
        {
            return await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int contactId)
        {
            return await _dbContext.Admins
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(c => c.ContactId, contactId));
        }
    }
}
