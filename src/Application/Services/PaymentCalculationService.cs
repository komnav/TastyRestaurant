using Application.Dtos.OrderDetail.Responses;
using Application.Dtos.PaymentCalculation.Responses;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public class PaymentCalculationService(
    IOrderDetailRepository orderDetailRepository)
    : IPaymentCalculationService
{
    private const int WaiterFeePercentage = 10;

    public async Task<PaymentCalculationResponseModel> PaymentCalculation(int orderId)
    {
        var orderDetails = await orderDetailRepository.GetAllByOrderIdAsync(orderId);
        if (orderDetails == null) throw new Exception("Order doesn't exist");

        var totalPrice = GetTotalPrice(orderDetails);
        var groupedDetail =await GroupByNameQuantityAndPrice(orderDetails);
        
        return new PaymentCalculationResponseModel(
            groupedDetail.MenuItemId,
            groupedDetail.Quantity,
            groupedDetail.Price,
            totalPrice
        );
    }

    private decimal GetTotalPrice(List<OrderDetail> details)
    {
        var calculatePrice = details.Sum(x => x.Price * x.Quantity);
        var waiterPercentage = (calculatePrice * WaiterFeePercentage) / 100;
        return waiterPercentage + calculatePrice;
    }

    private Task<GetOrderDetailResponseModel> GroupByNameQuantityAndPrice(List<OrderDetail> orderDetails)
    {
        foreach (var detail in orderDetails)
        {
            return Task.FromResult(new GetOrderDetailResponseModel(
                detail.Id,
                detail.OrderId,
                detail.MenuItemId,
                detail.Quantity,
                detail.Price,
                detail.Status
            ));
        }

        return (Task<GetOrderDetailResponseModel>)Task.CompletedTask;
    }
}