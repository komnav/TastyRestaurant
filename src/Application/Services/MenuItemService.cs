using Domain.Entities;
using Application.Dtos;
using Application.Dtos.MenuItem.Requests;
using Application.Dtos.MenuItem.Response;
using Application.Exceptions;
using Application.Repositories;

namespace Application.Services
{
    public class MenuItemService(IMenuItemRepository menuItemRepository) : IMenuItemService
    {
        public async Task<CreateMenuItemResponseModel> CreateAsync(CreateMenuItemRequestModel request)
        {
            var menuItem = new MenuItem
            {
                CategoryId = request.CategoryId,
                Price = request.Price,
                Name = request.Name
            };
            var rows = await menuItemRepository.CreateAsync(menuItem);

            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(menuItem));
            }

            return new CreateMenuItemResponseModel(menuItem.CategoryId, menuItem.Price, menuItem.Name, menuItem.Status);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await menuItemRepository.DeleteAsync(id);
        }

        public async Task<List<GetMenuItemResponseModel>> GetAll()
        {
            var menuItem = await menuItemRepository.GetAllAsync();

            return menuItem.Select(
                item => new GetMenuItemResponseModel(
                    item.Id,
                    item.CategoryId,
                    item.Price,
                    item.Name,
                    item.Status
                )).ToList();
        }

        public async Task<GetMenuItemResponseModel?> GetAsync(int id)
        {
            var menuItem = await menuItemRepository.GetAsync(id);

            if (menuItem == null)
            {
                return null;
            }

            return new GetMenuItemResponseModel(id, menuItem.CategoryId, menuItem.Price, menuItem.Name,
                menuItem.Status);
        }

        public async Task<List<GetMenuItemResponseModel>> GetByCategoryAsync(int categoryId)
        {
            var menuItems = await menuItemRepository.GetByCategoryAsync(categoryId);

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
            var rows = await menuItemRepository.UpdateAsync(id, request.CategoryId, request.Price, request.Name,
                request.Status);

            return new UpdateResponseModel(rows);
        }
    }
}