using Domain.Entities;

namespace RestaurantLayer.Services
{
    public interface IMenuItemService
    {
        Task<MenuItem> CreateAsync(MenuItem menuItem);
        Task<int> DeleteAsync(int id);
        Task<MenuItem> UpdateAsync(int id, MenuItem menuItem);
        Task<MenuItem> GetAsync(int id);
        Task<List<MenuItem>> GetAll();
    }
}
