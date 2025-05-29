using Domain.Entities;

namespace RestaurantLayer.Repositories
{
    public interface IMenuCategoryRepository
    {
        Task<int> CreateAsync(MenuCategory menuCategory);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(int id, string name, int? parentId);
        Task<MenuCategory?> GetAsync(int id);
        Task<List<MenuCategory>> GetAllAsync();
    }
}
