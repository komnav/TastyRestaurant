using Domain.Enums;

namespace Application.Dtos.Reservation.Requests;

public record UpdateReservationRequestModel(
    int TableId,
    int CustomerId,
    DateTime From,
    DateTime To,
    string? Notes,
    ReservationStatus Status
    );