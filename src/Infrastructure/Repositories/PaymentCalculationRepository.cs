using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentCalculationRepository(ApplicationDbContext dbContext) : IPaymentCalculationRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<OrderDetail?> CalculatePayment(int orderId)
    {
        return await
            _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}