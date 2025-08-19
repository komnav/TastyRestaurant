using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

namespace Infrastructure.Repositories
{
    public class OrderDetailRepository(ApplicationDbContext dbContext) : IOrderDetailRepository
    {
        public async Task<int> CreateAsync(OrderDetail orderDetail)
        {
            await dbContext.OrderDetails.AddAsync(orderDetail);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await dbContext.OrderDetails
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await dbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetAsync(int id)
        {
            return await dbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrderDetail>> GetAllByOrderIdAsync(int orderId)
        {
            return await dbContext.OrderDetails.Where(x => x.Id == orderId).ToListAsync();
        }

        public async Task<int> UpdateAsync(int id, int orderId, int menuItemId, int quantity, decimal price,
            OrderDetailStatus status)
        {
            return await dbContext.OrderDetails
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