using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(Contact contact, User user);
        Task<User> GetAsync(string username,string password);
    }
}
