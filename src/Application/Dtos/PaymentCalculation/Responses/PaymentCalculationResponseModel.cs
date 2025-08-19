namespace Application.Dtos.PaymentCalculation.Responses;

public record PaymentCalculationResponseModel(
    int IdMenuItem,
    int Quantity,
    decimal Price,
    decimal TotalPrice
);