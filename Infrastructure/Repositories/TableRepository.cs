using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class TableRepository(ApplicationDbContext dbContext) : ITableRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(Table table)
        {
            await _dbContext.Tables.AddAsync(table);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Table>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Table> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, int number, int capacity, TableType status)
        {
            throw new NotImplementedException();
        }
    }
}
