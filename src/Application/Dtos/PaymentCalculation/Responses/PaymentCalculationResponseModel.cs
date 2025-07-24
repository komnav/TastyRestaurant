namespace Application.Dtos.PaymentCalculation.Responses;

public record PaymentCalculationResponseModel(
    string MenuItemName,
    int Quantity,
    decimal Price,
    decimal TotalPrice
);