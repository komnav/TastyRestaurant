using Domain.Enums;

namespace Application.Dtos.Order.Requests;

public record UpdateOrderRequestModel(
    int TableId,
    DateTimeOffset DateTime,
    OrdersStatus Status
);