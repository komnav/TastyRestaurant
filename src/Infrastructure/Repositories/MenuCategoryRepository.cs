using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

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
            return await _dbContext.MenuCategories
                 .Where(x => x.Id == id)
                 .ExecuteDeleteAsync();
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

        public async Task<int> UpdateAsync(int id, string name, int? parentId)
        {
            return await _dbContext.MenuCategories
                   .Where(x => x.Id == id)
                   .ExecuteUpdateAsync(s => s
                   .SetProperty(x => x.ParentId, parentId)
                   .SetProperty(x => x.Name, name));
        }
    }
}
