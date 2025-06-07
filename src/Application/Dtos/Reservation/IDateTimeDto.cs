namespace RestaurantLayer.Dtos.Reservation;

public interface IDateTimeDto
{
    DateTimeOffset From { get; set; }
    DateTimeOffset To { get; set; }
}