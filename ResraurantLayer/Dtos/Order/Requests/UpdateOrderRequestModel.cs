using Domain.Enums;

namespace RestaurantLayer.Dtos.Order.Requests;

public record UpdateOrderRequestModel(
     int TableId,
     DateTime DateTime,
     OrdersStatus Status
    );