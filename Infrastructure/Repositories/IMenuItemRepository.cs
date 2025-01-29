using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IMenuItemRepository
    {
        Task<MenuItem> CreateAsync(MenuItem item);
        Task<int> DeleteAsync(int id);
        Task<MenuItem> GetAsync(int id);
        Task<List<MenuItem>> GetAllAsync();
        Task<MenuItem> UpdateAsync(int id, MenuItem item);
    }
}
