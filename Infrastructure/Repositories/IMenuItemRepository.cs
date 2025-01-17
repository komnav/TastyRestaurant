using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IMenuItemRepository
    {
        public Task CreateAsync(MenuItem item);
        public Task DeleteAsync(int id);
        public Task<MenuItem> GetAsync(int id);
        public Task UpdateAsync(int id, MenuItem item);
    }
}
