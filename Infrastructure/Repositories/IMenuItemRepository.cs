using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IMenuItemRepository
    {
        public Task CreateAsync(MenuItem item);
        public Task DeleteAsync(int id);
        public Task DeleteByCategory(int categoryId);
        public Task<MenuItem> GetAsync(int id);
        public Task UpdateAsync(int id, MenuItem item);
        public Task UpdateByCategory(int categoryId);
    }
}
