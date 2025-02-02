using Domain.Entities;
using Infrastructure.Repositories;

namespace ResraurantLayer.Services
{
    public class MenuItemService(IMenuItemRepository menuItemRepository) : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;
        public async Task<MenuItem> CreateAsync(MenuItem menuItem)
        {
            await _menuItemRepository.CreateAsync(menuItem);
            return menuItem;
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await _menuItemRepository.DeleteAsync(id);
        }

        public async Task<List<MenuItem>> GetAll()
        {
            return await _menuItemRepository.GetAllAsync();
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
