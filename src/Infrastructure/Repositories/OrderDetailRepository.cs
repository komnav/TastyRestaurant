using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using RestaurantLayer.Repositories;

namespace Infrastructure.Repositories
{
    public class OrderDetailRepository(ApplicationDbContext dbContext) : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<int> CreateAsync(OrderDetail orderDetail)
        {
            await _dbContext.OrderDetails.AddAsync(orderDetail);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.OrderDetails
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _dbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetAsync(int id)
        {
            return await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateAsync(int id, int orderId, int menuItemId, int quantity, decimal price, OrderDetailStatus status)
        {
            return await _dbContext.OrderDetails
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.OrderId, orderId)
                .SetProperty(x => x.MenuItemId, menuItemId)
                .SetProperty(x => x.Quantity, quantity)
                .SetProperty(x => x.Price, price)
                .SetProperty(x => x.Status, status));
        }
    }
}
