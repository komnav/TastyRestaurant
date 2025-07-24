using Application.Dtos.PaymentCalculation.Responses;

namespace Application.Services;

public interface IPaymentCalculationService
{
    Task<PaymentCalculationResponseModel> PaymentCalculation(int idOrder);
}