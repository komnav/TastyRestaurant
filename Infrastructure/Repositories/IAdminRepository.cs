using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IAdminRepository
    {
        Task<int> CreateAsync(Admin admin);
        Task<int> UpdateAsync(int id, int? contactId);
        Task<int> DeleteAsync(int id);
        Task<Admin> GetAsync(int id);
        Task<List<Admin>> GetAllAsync();
    }
}
