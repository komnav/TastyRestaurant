using Domain.Enums;

namespace Application.Dtos.Order.Requests;

public record UpdateOrderRequestModel(
    int UserId,
    DateTimeOffset DateTime,
    OrdersStatus Status
);