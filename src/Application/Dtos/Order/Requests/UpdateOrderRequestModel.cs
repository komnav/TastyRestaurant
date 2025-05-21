using Domain.Enums;

namespace Application.Dtos.Order.Requests;

public record UpdateOrderRequestModel(
     int TableId,
     DateTime DateTime,
     OrdersStatus Status
    );