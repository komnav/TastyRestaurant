using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CashierRepository(ApplicationDbContext dbContext) : ICashierRepository
    {
        ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Cashier cashier)
        {
            await _dbContext.Cashier.AddAsync(cashier);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Cashier
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Cashier>> GetAllAsync()
        {
            return await _dbContext.Cashier
                .ToListAsync();
        }

        public async Task<Cashier?> GetAsync(int id)
        {
            return await _dbContext.Cashier
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int contactId)
        {
            return await _dbContext.Cashier
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.ContactId, contactId));
        }
    }
}
