using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContactRepository(ApplicationDbContext dbContext) : IContactRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Contacts
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetAsync(int id)
        {
            return await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, string firstName, string lastName, string passportSeries, string phoneNumber, string email, string address)
        {
            return await _dbContext.Contacts
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.FirstName, firstName)
                .SetProperty(x => x.LastName, lastName)
                .SetProperty(x => x.PassportSeries, passportSeries)
                .SetProperty(x => x.Address, address)
                .SetProperty(x => x.Email, email)
                .SetProperty(x => x.PhoneNumber, phoneNumber));
        }
    }
}
