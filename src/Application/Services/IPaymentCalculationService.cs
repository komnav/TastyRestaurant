using Application.Dtos.PaymentCalculation.Responses;

namespace Application.Services;

public interface IPaymentCalculationService
{
    Task<List<PaymentCalculationResponseModel>> PaymentCalculation(int idOrder);
}