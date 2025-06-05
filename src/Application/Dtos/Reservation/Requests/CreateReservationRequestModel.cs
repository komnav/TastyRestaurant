using Domain.Enums;

namespace Application.Dtos.Reservation.Requests;

public record CreateReservationRequestModel(
    int TableId,
    int CustomerId,
    DateTimeOffset From,
    DateTimeOffset To,
    string? Notes,
    ReservationStatus Status
    );