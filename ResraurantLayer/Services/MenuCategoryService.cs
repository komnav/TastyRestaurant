using Domain.Entities;
using Infrastructure.Repositories;
using ResraurantLayer.Dtos.MenuCategory.Responses;
using ResraurantLayer.Exceptions;

namespace ResraurantLayer.Services
{
    public class MenuCategoryService(IMenuCategoryRepository menuCategoryRepository) : IMenuCategoryService
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
        public async Task<CreateMenuCategoryResponseModel> CreateAsync(string name)
        {
            var menuCategory = new MenuCategory
            {
                Name = name
            };


            var rows = await _menuCategoryRepository.CreateAsync(menuCategory);
            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(menuCategory), name);
            }

            return new CreateMenuCategoryResponseModel(menuCategory.Id, menuCategory.Name);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _menuCategoryRepository.DeleteAsync(id);
        }

        public Task<List<MenuCategory>> GetAllAsync()
        {
            return _menuCategoryRepository.GetAllAsync();
        }

        public async Task<MenuCategory?> GetAsync(int id)
        {
            return await _menuCategoryRepository.GetAsync(id);
        }

        public async Task<MenuCategory> UpdateAsync(int id, MenuCategory menuCategory)
        {
            return await _menuCategoryRepository.UpdateAsync(id, menuCategory);
        }
    }
}
