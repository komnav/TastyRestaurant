using Domain.Enums;

namespace Application.Dtos.Order.Requests;

public record CreateOrderRequestModel(
     int TableId,
     DateTime DateTime,
     OrdersStatus Status
    );
