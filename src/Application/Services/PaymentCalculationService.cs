using Application.Dtos.PaymentCalculation.Responses;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public class PaymentCalculationService(
    IOrderDetailRepository orderDetailRepository)
    : IPaymentCalculationService
{
    private const int WaiterFeePercentage = 10;

    public async Task<List<PaymentCalculationResponseModel>> PaymentCalculation(int orderId)
    {
        var orderDetails = await orderDetailRepository.GetAllByOrderIdAsync(orderId);
        if (orderDetails == null) throw new Exception("Order doesn't exist");

        var totalPrice = GetTotalPrice(orderDetails);
        
        return orderDetails.Select(orderDetail => new PaymentCalculationResponseModel(
            orderDetail.MenuItemId,
            orderDetail.Quantity,
            orderDetail.Price,
            totalPrice,
            WaiterFeePercentage
            )).ToList();
    }

    private decimal GetTotalPrice(List<OrderDetail> details)
    {
        var calculatePrice = details.Sum(x => x.Price * x.Quantity);
        var waiterPercentage = (calculatePrice * WaiterFeePercentage) / 100;
        return waiterPercentage + calculatePrice;
    }
}