using Domain.Entities;

namespace RestaurantLayer.Repositories
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(Contact contact, User? user);
        Task<User?> GetAsync(string username,string password);
    }
}
