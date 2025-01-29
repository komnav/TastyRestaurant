using Domain.Entities;
using Infrastructure.Repositories;

namespace ResraurantLayer.Services
{
    public class MenuItemService(MenuItemRepository menuItemRepository) : IMenuItemService
    {
        private readonly MenuItemRepository _menuItemRepository = menuItemRepository;
        public async Task<MenuItem> CreateAsync(MenuItem menuItem)
        {
            await _menuItemRepository.CreateAsync(menuItem);
            return menuItem;
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await _menuItemRepository.DeleteAsync(id);
        }

        public async Task<MenuItem> GetAsync(int id)
        {
            return await _menuItemRepository.GetAsync(id);
        }

        public async Task<MenuItem> UpdateAsync(int id, MenuItem menuItem)
        {
            return await _menuItemRepository.UpdateAsync(id, menuItem);
        }
    }
}
