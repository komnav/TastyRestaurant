using Domain.Enums;

namespace RestaurantLayer.Dtos.OrderDetail.Responses;

public record GetOrderDetailResponseModel(
    int Id,
    int OrderId,
    int MenuItemId,
    int Quantity,
    decimal Price,
    OrderDetailStatus Status
    );