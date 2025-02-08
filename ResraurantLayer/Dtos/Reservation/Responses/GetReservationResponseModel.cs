using Domain.Enums;

namespace RestaurantLayer.Dtos.Reservation.Responses;

public record GetReservationResponseModel(
    int Id,
    int TableId,
    int CustomerId,
    DateTime From,
    DateTime To,
    string? Notes,
    ReservationStatus Status
    );
