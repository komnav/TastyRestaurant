using Application.Dtos.PaymentCalculation.Responses;
using Application.Repositories;

namespace Application.Services;

public class PaymentCalculationService(
    IPaymentCalculationRepository calculationRepository,
    IMenuItemRepository menuItemRepository)
    : IPaymentCalculationService
{
    public async Task<PaymentCalculationResponseModel> PaymentCalculation(int idOrder)
    {
        var orderDetail = await calculationRepository.CalculatePayment(idOrder);
        if (orderDetail == null) throw new Exception("Order doesn't exist");

        var menuItem = await menuItemRepository.GetAsync(orderDetail!.MenuItemId);
        if (menuItem == null) throw new Exception("Menu item doesn't exist");

        var calculatePrice = menuItem!.Price * orderDetail.Quantity;
        return new PaymentCalculationResponseModel(
            menuItem!.Name,
            orderDetail.Quantity,
            menuItem.Price,
            calculatePrice
        );
    }
}