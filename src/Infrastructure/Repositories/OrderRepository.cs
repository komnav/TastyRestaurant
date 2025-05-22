using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Order?> GetAsync(int id)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> CreateAsync(Order order)
        {
            await _dbContext.AddAsync(order);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(int id, int tableId, DateTime dateTime, OrdersStatus ordersStatus)
        {
            return await _dbContext.Orders
                  .Where(x => x.Id == id)
                  .ExecuteUpdateAsync(s => s
                  .SetProperty(s => s.Id, id)
                  .SetProperty(s => s.DateTime, dateTime)
                  .SetProperty(s => s.Status, ordersStatus)
                  .SetProperty(s => s.TableId, tableId));
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await _dbContext.Orders
                 .Where(x => x.Id == id)
                 .ExecuteDeleteAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }
    }
}
