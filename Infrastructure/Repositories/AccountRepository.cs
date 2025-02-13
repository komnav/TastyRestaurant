using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository(ApplicationDbContext dbContext) : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Contact contact, User user)
        {
            await _dbContext.Contacts.AddAsync(contact);

            await _dbContext.Users.AddAsync(user);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
