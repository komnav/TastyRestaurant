using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MenuCategoryRepository(ApplicationDbContext dbContext) : IMenuCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(MenuCategory menuCategory)
        {
            await _dbContext.MenuCategories.AddAsync(menuCategory);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await _dbContext.MenuCategories
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<List<MenuCategory>> GetAllAsync()
        {
            return await _dbContext.MenuCategories.ToListAsync();
        }

        public async Task<MenuCategory?> GetAsync(int id)
        {
            return await _dbContext.MenuCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<List<MenuCategory>> GetByCategories(int parentId)
        {
            var categories = await _dbContext.MenuCategories
                .Where(x => x.ParentId == parentId)
                .ToListAsync();
            return categories;
        }

        public async Task<int> UpdateAsync(int id, MenuCategory menuCategory)
        {
            await _dbContext.MenuCategories
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.ParentId, menuCategory.ParentId)
                .SetProperty(x => x.Name, menuCategory.Name));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
