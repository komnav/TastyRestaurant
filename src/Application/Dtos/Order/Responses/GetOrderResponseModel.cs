using Domain.Enums;

namespace Application.Dtos.Order.Responses;

public record GetOrderResponseModel(
     int Id,
     int tableId,
     DateTime dateTime,
     OrdersStatus ordersStatus
    );