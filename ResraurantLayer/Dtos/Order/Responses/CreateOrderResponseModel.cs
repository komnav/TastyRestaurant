using Domain.Enums;

namespace RestaurantLayer.Dtos.Order.Responses;

public record CreateOrderResponseModel(
     int Id,
     int TableId,
     DateTime DateTime,
     OrdersStatus OrdersStatus
    );
