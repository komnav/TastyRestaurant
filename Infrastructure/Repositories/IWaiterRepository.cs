using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IWaiterRepository
    {
        Task<int> CreateAsync(Waiter waiter);
        Task<int> UpdateAsync(int id, int? contactId);
        Task<int> DeleteAsync(int id);
        Task<Waiter?> GetAsync(int id);
        Task<List<Waiter>> GetAllAsync();
    }
}
