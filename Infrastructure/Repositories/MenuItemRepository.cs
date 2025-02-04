using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Infrastructure.Repositories
{
    public class MenuItemRepository(ApplicationDbContext dbContext) : IMenuItemRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(MenuItem item)
        {
            await _dbContext.MenuItems.AddAsync(item);
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.MenuItems
                   .Where(x => x.Id.Equals(id))
                   .ExecuteDeleteAsync();
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _dbContext.MenuItems.ToListAsync();
        }

        public async Task<MenuItem> GetAsync(int id)
        {
            return await _dbContext.MenuItems.SingleAsync(x => x.Id.Equals(id));

        }

        public async Task<int> UpdateAsync(int id, int categoryId, decimal price, string name, MenuItemStatus status)
        {
            return await _dbContext.MenuItems
                   .Where(x => x.Id == id)
                   .ExecuteUpdateAsync(x => x
                   .SetProperty(x => x.Price, price)
                   .SetProperty(x => x.CategoryId, categoryId)
                   .SetProperty(x => x.Name, name)
                   .SetProperty(x => x.Status, status));

        }
    }
}
