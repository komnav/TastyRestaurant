using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IContactRepository
    {
        Task<int> CreateAsync(Contact contact);
        Task<int> UpdateAsync(int id, string firstName, string lastName, string passportSeries, string phoneNumber, string email, string address);
        Task<int> DeleteAsync(int id);
        Task<Contact?> GetAsync(int id);
        Task<List<Contact>> GetAllAsync();
    }
}
