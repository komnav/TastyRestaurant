using Domain.Enums;

namespace RestaurantLayer.Dtos.Reservation.Requests;

public record CreateReservationRequestModel(
    int TableId,
    int CustomerId,
    DateTime From,
    DateTime To,
    string? Notes,
    ReservationStatus Status
    );