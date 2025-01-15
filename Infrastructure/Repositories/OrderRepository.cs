
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> Get()
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Order?> GetById(int id)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<List<Order>> GetByFilter(DateTime dateTime, OrdersStatus status)
        {
            var query = _dbContext.Orders.AsNoTracking();

            if (!string.IsNullOrEmpty(dateTime.ToString()))
            {
                query = query.Where(x => x.DateTime == dateTime);
            }
            if (!string.IsNullOrEmpty(status.ToString()))
            {
                query = query.Where(x => x.Status == status);
            }
            return await query.ToListAsync();
        }

        public async Task Add(int id, DateTime dateTime, OrdersStatus ordersStatus)
        {
            var orderEntity = new Order
            {
                Id = id,
                DateTime = dateTime,
                Status = ordersStatus
            };
            await _dbContext.AddAsync(orderEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(int id, DateTime dateTime, OrdersStatus ordersStatus)
        {
            await _dbContext.Orders
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.Id, id)
                .SetProperty(s => s.DateTime, dateTime)
                .SetProperty(s => s.Status, ordersStatus));
        }
        public async Task Delete(int id)
        {
            await _dbContext.Orders
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
