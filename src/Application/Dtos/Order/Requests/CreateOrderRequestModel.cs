using Domain.Enums;

namespace Application.Dtos.Order.Requests;

public record CreateOrderRequestModel(
    int TableId,
    DateTimeOffset DateTime,
    OrdersStatus Status
);