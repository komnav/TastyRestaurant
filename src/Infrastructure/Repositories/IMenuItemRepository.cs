using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public interface IMenuItemRepository
    {
        Task<int> CreateAsync(MenuItem item);
        Task<int> DeleteAsync(int id);
        Task<MenuItem?> GetAsync(int id);
        Task<List<MenuItem>> GetAllAsync();
        Task<List<MenuItem>> GetByCategoryAsync(int categoryId);
        Task<int> UpdateAsync(int id, int parentId, decimal price, string name, MenuItemStatus status);
    }
}
