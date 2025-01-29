using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IMenuCategoryRepository
    {
         Task<MenuCategory> CreateAsync(MenuCategory menuCategory);
         Task<int> DeleteAsync(int id);
         Task<MenuCategory> UpdateAsync(int id, MenuCategory menuCategory);
         Task<MenuCategory> GetAsync(int id);
    }
}
