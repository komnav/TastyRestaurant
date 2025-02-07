using Domain.Enums;

namespace RestaurantLayer.Dtos.Order.Requests;

public record CreateOrderRequestModel(
     int TableId,
     DateTime DateTime,
     OrdersStatus Status
    );
