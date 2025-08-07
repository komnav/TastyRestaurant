using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;

namespace Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
    {
        public async Task<Order?> GetAsync(int id)
        {
            return await dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> CreateAsync(Order order)
        {
            await dbContext.AddAsync(order);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(
            int id,
            int userId,
            DateTimeOffset dateTime,
            OrdersStatus ordersStatus)
        {
            return await dbContext.Orders
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(order => order.Id, id)
                    .SetProperty(order => order.DateTime, dateTime)
                    .SetProperty(order => order.Status, ordersStatus)
                    .SetProperty(order => order.UserId, userId));
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await dbContext.Orders
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await dbContext.Orders.ToListAsync();
        }
    }
}