using Domain.Enums;

namespace Application.Dtos.Order.Requests;

public record CreateOrderRequestModel(
    int UserId,
    DateTimeOffset DateTime,
    OrdersStatus Status
);