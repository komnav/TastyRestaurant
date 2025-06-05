using Domain.Enums;

namespace Application.Dtos.Reservation.Responses;

public record CreateReservationResponseModel(
    int Id,
    int TableId,
    int CustomerId,
    DateTimeOffset From,
    DateTimeOffset To,
    string? Notes,
    ReservationStatus Status
    );