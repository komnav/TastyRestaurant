using Domain.Entities;
using Infrastructure.Repositories;

namespace ResraurantLayer.Services
{
    public class MenuItemService(MenuItemRepository menuItemRepository) : IMenuItemService
    {
        private readonly MenuItemRepository _menuItemRepository = menuItemRepository;
        public async Task CreateAsync(MenuItem menuItem)
        {
            await _menuItemRepository.CreateAsync(menuItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _menuItemRepository.DeleteAsync(id);
        }

        public async Task<MenuItem> GetAsync(int id)
        {
            return await _menuItemRepository.GetAsync(id);
        }

        public async Task UpdateAsync(int id, MenuItem menuItem)
        {
            await _menuItemRepository.UpdateAsync(id, menuItem);
        }
    }
}
