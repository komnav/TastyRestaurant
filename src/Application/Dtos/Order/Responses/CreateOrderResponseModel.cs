using Domain.Enums;

namespace Application.Dtos.Order.Responses;

public record CreateOrderResponseModel(
     int Id,
     int TableId,
     DateTimeOffset DateTime,
     OrdersStatus OrdersStatus
    );
