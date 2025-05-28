using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Tables
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Table>> GetAllAsync()
        {
            return await _dbContext.Tables.ToListAsync();
        }

        public async Task<Table?> GetAsync(int id)
        {
            return await _dbContext.Tables.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int number, int capacity, TableType type)
        {
            return await _dbContext.Tables
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(table => table.Number, number)
                    .SetProperty(table => table.Capacity, capacity)
                    .SetProperty(table => table.Type, type
                    ));
        }
    }
}