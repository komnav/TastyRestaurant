using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateAsync(Order order);
        Task<int> UpdateAsync(int id, int tableId, DateTime dateTime, OrdersStatus status);
        Task<int> DeleteAsync(int id);
        Task<Order?> GetAsync(int id);
        Task<List<Order>> GetAllAsync();

    }
}
