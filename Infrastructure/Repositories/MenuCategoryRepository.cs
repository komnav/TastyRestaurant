using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MenuCategoryRepository(ApplicationDbContext dbContext) : IMenuCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<MenuCategory> CreateAsync(MenuCategory menuCategory)
        {
            await _dbContext.MenuCategories.AddAsync(menuCategory);
            await _dbContext.SaveChangesAsync();
            return menuCategory;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await _dbContext.MenuCategories.Where(x => x.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<MenuCategory> GetAsync(int id)
        {
            return await _dbContext.MenuCategories.SingleAsync(x => x.Id.Equals(id));
        }

        public async Task<MenuCategory> UpdateAsync(int id, MenuCategory menuCategory)
        {
            await _dbContext.MenuCategories
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.ParentId, menuCategory.ParentId)
                .SetProperty(x => x.Name, menuCategory.Name));
            return menuCategory;
        }
    }
}
