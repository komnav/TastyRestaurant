using Application.Dtos.PaymentCalculation.Responses;
using Application.Repositories;

namespace Application.Services;

public class PaymentCalculationService(
    IMenuItemRepository menuItemRepository,
    IOrderDetailRepository orderDetailRepository)
    : IPaymentCalculationService
{
    public async Task<PaymentCalculationResponseModel> PaymentCalculation(int idOrder)
    {
        var orderDetails = await orderDetailRepository.GetAllAsync();
        if (orderDetails == null) throw new Exception("Order doesn't exist");

        var orderDetail = orderDetails.FirstOrDefault(x => x.OrderId == idOrder);

        var menuItem = await menuItemRepository.GetAsync(orderDetail!.MenuItemId);
        if (menuItem == null) throw new Exception("Menu item doesn't exist");

        var calculatePrice = menuItem.Price * orderDetail.Quantity;
        return new PaymentCalculationResponseModel(
            menuItem.Name,
            orderDetail.Quantity,
            menuItem.Price,
            calculatePrice
        );
    }
}