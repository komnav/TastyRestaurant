using Domain.Enums;

namespace Application.Dtos.OrderDetail.Responses;

public record CreateOrderDetailResponseModel(
    int Id,
    int OrderId,
    int MenuItemId,
    int Quantity,
    decimal Price,
    OrderDetailStatus Status
    );