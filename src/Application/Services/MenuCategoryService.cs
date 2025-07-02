using Application.Dtos.MenuCategory.Responses;
using Domain.Entities;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.MenuCategory.Requests;
using RestaurantLayer.Exceptions;
using RestaurantLayer.Repositories;

namespace RestaurantLayer.Services
{
    public class MenuCategoryService(
        IMenuCategoryRepository menuCategoryRepository
    ) : IMenuCategoryService
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

        public async Task<List<GetMenuCategoryResponseModel>> GetAllAsync()
        {
            var menuCategory = await _menuCategoryRepository.GetAllAsync();

            return menuCategory.Select(menuCategory => new GetMenuCategoryResponseModel(
                menuCategory.Id,
                menuCategory.Name,
                menuCategory.ParentId
            )).ToList();
        }

        public async Task<GetMenuCategoryResponseModel?> GetAsync(int id)
        {
            var menuCategory = await _menuCategoryRepository.GetAsync(id);

            if (menuCategory == null)
            {
                return null;
            }

            return new GetMenuCategoryResponseModel(
                menuCategory.Id,
                menuCategory.Name,
                menuCategory.ParentId);
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateMenuCategoryRequestModel request)
        {
            var rows = await _menuCategoryRepository.UpdateAsync(id, request.Name, request.ParentId);

            return new UpdateResponseModel(rows);
        }
    }
}