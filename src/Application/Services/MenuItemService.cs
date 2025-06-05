using Domain.Entities;
using Infrastructure.Repositories;
using Application.Dtos;
using Application.Dtos.MenuItem.Requests;
using Application.Dtos.MenuItem.Response;
using Application.Services;
using RestaurantLayer.Exceptions;
using RestaurantLayer.Repositories;

namespace ResraurantLayer.Services
{
    public class MenuItemService(IMenuItemRepository menuItemRepository) : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;

        public async Task<CreateMenuItemResponseModel> CreateAsync(CreateMenuItemRequestModel request)
        {
            var menuItem = new MenuItem
            {
                CategoryId = request.CategoryId,
                Price = request.Price,
                Name = request.Name
            };
            var rows = await _menuItemRepository.CreateAsync(menuItem);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(menuItem));
            }

            return new CreateMenuItemResponseModel(menuItem.CategoryId, menuItem.Price, menuItem.Name, menuItem.Status);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _menuItemRepository.DeleteAsync(id);
        }

        public async Task<List<GetMenuItemResponseModel>> GetAll()
        {
            var menuItem = await _menuItemRepository.GetAllAsync();

            return menuItem.Select(menuItem => new GetMenuItemResponseModel(
                menuItem.Id,
                menuItem.CategoryId,
                menuItem.Price,
                menuItem.Name,
                menuItem.Status
            )).ToList();
        }

        public async Task<GetMenuItemResponseModel?> GetAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetAsync(id);

            if (menuItem == null)
            {
                return null;
            }

            return new GetMenuItemResponseModel(id, menuItem.CategoryId, menuItem.Price, menuItem.Name,
                menuItem.Status);
        }

        public async Task<List<GetMenuItemResponseModel>> GetByCategoryAsync(int categoryId)
        {
            var menuItems = await _menuItemRepository.GetByCategoryAsync(categoryId);

            return menuItems.Select(menuItem => new GetMenuItemResponseModel(
                menuItem.Id,
                menuItem.CategoryId,
                menuItem.Price,
                menuItem.Name,
                menuItem.Status
            )).ToList();
        }

        public async Task<UpdateResponseModel> UpdateAsync(int id, UpdateMenuItemRequestModel request)
        {
            var rows = await _menuItemRepository.UpdateAsync(id, request.CategoryId, request.Price, request.Name,
                request.Status);

            return new UpdateResponseModel(rows);
        }
    }
}