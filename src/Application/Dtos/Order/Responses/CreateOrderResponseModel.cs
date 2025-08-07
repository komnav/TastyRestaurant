using Domain.Enums;

namespace Application.Dtos.Order.Responses;

public record CreateOrderResponseModel(
     int Id,
     DateTimeOffset DateTime,
     OrdersStatus OrdersStatus
    );
