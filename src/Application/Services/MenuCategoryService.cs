using Domain.Entities;
using Application.Dtos;
using Application.Dtos.MenuCategory.Requests;
using Application.Dtos.MenuCategory.Responses;
using Application.Exceptions;
using Application.Repositories;

namespace Application.Services
{
    public class MenuCategoryService(
        IMenuCategoryRepository menuCategoryRepository
    ) : IMenuCategoryService
    {
        public async Task<CreateMenuCategoryResponseModel> CreateAsync(string name)
        {
            var menuCategory = new MenuCategory
            {
                Name = name
            };

            var rows = await menuCategoryRepository.CreateAsync(menuCategory);
            if (rows <= 0)
            {
                throw new ResourceWasNotCreatedException(nameof(menuCategory), name);
            }

            return new CreateMenuCategoryResponseModel(menuCategory.Id, menuCategory.Name);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await menuCategoryRepository.DeleteAsync(id);
        }

        public async Task<List<GetMenuCategoryResponseModel>> GetAllAsync()
        {
            var menuCategory = await menuCategoryRepository.GetAllAsync();

            return menuCategory.Select(
                category => new GetMenuCategoryResponseModel(
                    category.Id,
                    category.Name,
                    category.ParentId
                )).ToList();
        }

        public async Task<GetMenuCategoryResponseModel?> GetAsync(int id)
        {
            var menuCategory = await menuCategoryRepository.GetAsync(id);

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
            var rows = await menuCategoryRepository.UpdateAsync(id, request.Name, request.ParentId);

            return new UpdateResponseModel(rows);
        }
    }
}