using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface ICookerRepository
    {
        Task<int> CreateAsync(Cooker cooker);
        Task<int> UpdateAsync(int id, int? contactId);
        Task<int> DeleteAsync(int id);
        Task<Cooker?> GetAsync(int id);
        Task<List<Cooker>> GetAllAsync();
    }
}
