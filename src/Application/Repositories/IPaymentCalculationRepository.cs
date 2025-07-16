using Domain.Entities;

namespace Application.Repositories;

public interface IPaymentCalculationRepository
{
    Task<OrderDetail?> CalculatePayment(int orderId);
}