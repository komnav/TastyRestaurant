using Domain.Enums;

namespace Application.Dtos.Order.Responses;

public record GetOrderResponseModel(
     int Id,
     int tableId,
     DateTimeOffset dateTime,
     OrdersStatus ordersStatus
    );