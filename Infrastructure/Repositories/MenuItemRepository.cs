using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MenuItemRepository(ApplicationDbContext dbContext) : IMenuItemRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<MenuItem> CreateAsync(MenuItem item)
        {
            await _dbContext.MenuItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
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

        public async Task<MenuItem> UpdateAsync(int id, MenuItem item)
        {
            await _dbContext.MenuItems
                  .Where(x => x.Id == id)
                  .ExecuteUpdateAsync(x => x
                  .SetProperty(x => x.Price, item.Price)
                  .SetProperty(x => x.CategoryId, item.CategoryId)
                  .SetProperty(x => x.Name, item.Name)
                  .SetProperty(x => x.Status, item.Status));
            return item;
        }
    }
}
