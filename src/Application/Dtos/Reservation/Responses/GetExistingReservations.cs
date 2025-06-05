namespace Application.Dtos.Reservation.Responses;

public record GetExistingReservations(
    int TableId,
    DateTimeOffset From,
    DateTimeOffset To
);