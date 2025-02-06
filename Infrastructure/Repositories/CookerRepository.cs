using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CookerRepository(ApplicationDbContext dbContext) : ICookerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Cooker cooker)
        {
            await _dbContext.Cookers.AddAsync(cooker);
            return _dbContext.SaveChanges();
        }

        public Task<int> DeleteAsync(int id)
        {
            return _dbContext.Cookers
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Cooker>> GetAllAsync()
        {
            return await _dbContext.Cookers.ToListAsync();
        }

        public async Task<Cooker?> GetAsync(int id)
        {
            return await _dbContext.Cookers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int contactId)
        {
            return await _dbContext.Cookers
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.ContactId, contactId));
        }
    }
}
