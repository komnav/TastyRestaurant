using Domain.Entities;

namespace ResraurantLayer.Services
{
    public interface IMenuItemService
    {
        public Task CreateAsync(MenuItem menuItem);
        public Task UpdateByCategoryAsync(int categoryId);
        public Task DeleteAsync(int id);
        public Task DeleteByCategoryAsync(int categoryId);
        public Task UpdateAsync(int id, MenuItem menuItem);
        public Task<MenuItem> GetAsync(int id);
    }
}
