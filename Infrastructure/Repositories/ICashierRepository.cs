using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface ICashierRepository
    {
        Task<int> CreateAsync(Cashier cashier);
        Task<int> UpdateAsync(int id, int? contactId);
        Task<int> DeleteAsync(int id);
        Task<Cashier?> GetAsync(int id);
        Task<List<Cashier>> GetAllAsync();
    }
}
