using Domain.Entities;
using Infrastructure;

namespace ResraurantLayer.Services
{
    public class MenuCategoryService(MenuCategoryService menuCategory) : IMenuCategoryService
    {
        private readonly IMenuCategoryService _menuCategory = menuCategory;
        public async Task CreateAsync(MenuCategory menuCategory)
        {
            await _menuCategory.CreateAsync(menuCategory);
        }

        public async Task DeleteAsync(int id)
        {
            await _menuCategory.DeleteAsync(id);
        }

        public async Task<MenuItem> GetAsync(int id)
        {
            return await _menuCategory.GetAsync(id);
        }

        public async Task UpdateAsync(int id, MenuCategory menuCategory)
        {
            await _menuCategory.UpdateAsync(id, menuCategory);
        }
    }
}
