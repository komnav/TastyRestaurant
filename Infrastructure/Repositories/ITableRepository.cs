using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public interface ITableRepository
    {
        Task<int> CreateAsync(Table table);
        Task<int> UpdateAsync(int id, int number, int capacity, TableType status);
        Task<int> DeleteAsync(int id);
        Task<Table> GetAsync(int id);
        Task<List<Table>> GetAllAsync();
    }
}
