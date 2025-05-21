using Domain.Enums;

namespace Application.Dtos.OrderDetail.Requests;

public record CreateOrderDetailRequestModel(
    int OrderId,
    int MenuItemId,
    int Quantity,
    decimal Price,
    OrderDetailStatus Status
    );