using Domain.Enums;

namespace Application.Dtos.Reservation.Responses;

public record CreateReservationResponseModel(
    int Id,
    int TableId,
    int CustomerId,
    DateTime From,
    DateTime To,
    string? Notes,
    ReservationStatus Status
    );