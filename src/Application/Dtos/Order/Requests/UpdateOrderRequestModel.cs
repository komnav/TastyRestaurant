using Domain.Enums;

namespace RestaurantLayer.Dtos.Order.Requests;

public record UpdateOrderRequestModel(
    int TableId,
    DateTimeOffset DateTime,
    OrdersStatus Status
);