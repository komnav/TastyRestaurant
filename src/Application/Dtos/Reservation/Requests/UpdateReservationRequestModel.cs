using Domain.Enums;

namespace Application.Dtos.Reservation.Requests;

public record UpdateReservationRequestModel(
    int TableId,
    int CustomerId,
    DateTimeOffset From,
    DateTimeOffset To,
    string? Notes,
    ReservationStatus Status
    );