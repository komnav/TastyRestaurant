using Domain.Enums;

namespace RestaurantLayer.Dtos.Order.Responses;

public record GetOrderResponseModel(
     int Id,
     int tableId,
     DateTime dateTime,
     OrdersStatus ordersStatus
    );