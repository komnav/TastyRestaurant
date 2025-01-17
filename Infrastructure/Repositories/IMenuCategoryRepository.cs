using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IMenuCategoryRepository
    {
        public Task CreateAsync(MenuCategory menuCategory);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, MenuCategory menuCategory);
        public Task<MenuCategory> GetAsync(int id);
    }
}
