using Domain.Entities;
using Infrastructure;

namespace ResraurantLayer.Services
{
    public class MenuCategoryService(MenuCategoryService menuCategory) : IMenuCategoryService
    {
        private readonly IMenuCategoryService _menuCategory = menuCategory;
        public async Task<MenuCategory> CreateAsync(MenuCategory menuCategory)
        {
            return await _menuCategory.CreateAsync(menuCategory);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _menuCategory.DeleteAsync(id);
        }

        public Task<List<MenuCategory>> GetAllAsync()
        {
            return _menuCategory.GetAllAsync();
        }

        public async Task<MenuCategory> GetAsync(int id)
        {
            return await _menuCategory.GetAsync(id);
        }

        public async Task<MenuCategory> UpdateAsync(int id, MenuCategory menuCategory)
        {
            return await _menuCategory.UpdateAsync(id, menuCategory);
        }
    }
}
