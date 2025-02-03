using Domain.Entities;
using RestaurantLayer.Dtos;
using RestaurantLayer.Dtos.MenuCategory.Requests;
using RestaurantLayer.Dtos.MenuCategory.Responses;

namespace ResraurantLayer.Services
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
