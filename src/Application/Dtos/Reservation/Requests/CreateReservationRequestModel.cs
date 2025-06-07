using Domain.Enums;
using RestaurantLayer.Dtos.Reservation;

namespace Application.Dtos.Reservation.Requests;

public record CreateReservationRequestModel : IDateTimeDto
{
    public int TableId { get; init; }
    public int CustomerId { get; init; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
    public string? Notes { get; init; }
    public ReservationStatus Status { get; init; }
}