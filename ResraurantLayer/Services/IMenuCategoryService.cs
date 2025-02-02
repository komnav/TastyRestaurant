using Domain.Entities;
using ResraurantLayer.Dtos.MenuCategory.Requests;
using ResraurantLayer.Dtos.MenuCategory.Responses;

namespace ResraurantLayer.Services
{
    public interface IMenuCategoryService
    {
        Task<CreateMenuCategoryResponseModel> CreateAsync(string name);
        Task<int> DeleteAsync(int id);
        Task<MenuCategory> UpdateAsync(int id, MenuCategory menuCategory);
        Task<MenuCategory?> GetAsync(int id);
        Task<List<MenuCategory>> GetAllAsync();
    }
}
