using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WaiterRepository(ApplicationDbContext dbContext) : IWaiterRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Waiter waiter)
        {
            await _dbContext.Waiters.AddAsync(waiter);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Waiters.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Waiter>> GetAllAsync()
        {
            return await _dbContext.Waiters.ToListAsync();
        }

        public async Task<Waiter?> GetAsync(int id)
        {
            return await _dbContext.Waiters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int? contactId)
        {
            return await _dbContext.Waiters
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.ContactId, contactId));
        }
    }
}
