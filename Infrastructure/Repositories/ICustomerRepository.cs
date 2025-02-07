using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> CreateAsync(Customer customer);
        Task<int> UpdateAsync(int id, int contactId);
        Task<int> DeleteAsync(int id);
        Task<Customer?> GetAsync(int id);
        Task<List<Customer>> GetAllAsync();
    }
}
