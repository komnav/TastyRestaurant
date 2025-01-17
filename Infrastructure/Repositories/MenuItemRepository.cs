using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MenuItemRepository(ApplicationDbContext dbContext) : IMenuItemRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task CreateAsync(MenuItem item)
        {
            await _dbContext.MenuItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.MenuItems.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByCategoryAsync(int categoryId)
        {
            await _dbContext.MenuItems.Where(x => x.CategoryId == categoryId).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MenuItem> GetAsync(int id)
        {
            return await _dbContext.MenuItems.SingleAsync(x => x.Id.Equals(id));

        }

        public async Task UpdateAsync(int id, MenuItem item)
        {
            await _dbContext.MenuItems
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.Price, item.Price)
                .SetProperty(x => x.CategoryId, item.CategoryId)
                .SetProperty(x => x.Name, item.Name)
                .SetProperty(x => x.Status, item.Status));
        }

        public async Task UpdateByCategoryAsync(int categoryId)
        {
            await _dbContext.MenuItems
                .Where(x => x.CategoryId == categoryId)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.CategoryId, categoryId));
        }
    }
}
