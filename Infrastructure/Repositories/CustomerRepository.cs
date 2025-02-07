using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Customers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetAsync(int id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int contactId)
        {
            return await _dbContext.Customers
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.ContactId, contactId));
        }
    }
}
