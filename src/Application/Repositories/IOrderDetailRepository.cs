using Domain.Entities;
using Domain.Enums;

namespace RestaurantLayer.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<int> CreateAsync(OrderDetail orderDetail);
        Task<int> UpdateAsync(int id, int orderId, int menuItemId, int quantity, decimal price, OrderDetailStatus status);
        Task<int> DeleteAsync(int id);
        Task<OrderDetail?> GetAsync(int id);
        Task<List<OrderDetail>> GetAllAsync();
    }
}
