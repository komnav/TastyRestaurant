using Domain.Entities;

namespace ResraurantLayer.Services
{
    public interface IMenuCategoryService
    {
        Task<MenuCategory> CreateAsync(MenuCategory menuCategory);
        Task<int> DeleteAsync(int id);
        Task<MenuCategory> UpdateAsync(int id, MenuCategory menuCategory);
        Task<MenuCategory> GetAsync(int id);
        Task<List<MenuCategory>> GetAllAsync();
    }
}
