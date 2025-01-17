using Domain.Entities;

namespace ResraurantLayer.Services
{
    public interface IMenuCategoryService
    {
        public Task CreateAsync(MenuCategory menuCategory);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, MenuCategory menuCategory);
        public Task<MenuItem> GetAsync(int id);
    }
}
