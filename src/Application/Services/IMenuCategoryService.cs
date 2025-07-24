using Application.Dtos;
using Application.Dtos.MenuCategory.Requests;
using Application.Dtos.MenuCategory.Responses;

namespace Application.Services
{
    public interface IMenuCategoryService
    {
        Task<CreateMenuCategoryResponseModel> CreateAsync(string name);
        Task<int> DeleteAsync(int id);
        Task<UpdateResponseModel> UpdateAsync(int id, UpdateMenuCategoryRequestModel menuCategory);
        Task<GetMenuCategoryResponseModel?> GetAsync(int id);
        Task<List<GetMenuCategoryResponseModel>> GetAllAsync();
    }
}