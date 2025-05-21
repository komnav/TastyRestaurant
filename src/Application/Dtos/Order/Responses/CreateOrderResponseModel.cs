using Domain.Enums;

namespace Application.Dtos.Order.Responses;

public record CreateOrderResponseModel(
     int Id,
     int TableId,
     DateTime DateTime,
     OrdersStatus OrdersStatus
    );
