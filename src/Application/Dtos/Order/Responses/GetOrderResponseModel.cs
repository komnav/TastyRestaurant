using Domain.Enums;

namespace Application.Dtos.Order.Responses;

public record GetOrderResponseModel(
     int Id,
     int UserId,
     DateTimeOffset dateTime,
     OrdersStatus ordersStatus
    );