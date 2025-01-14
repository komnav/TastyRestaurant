using Domain.Enums;

namespace Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public required Table Table { get; set; }
        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }
        public required DateTime From { get; set; }
        public required DateTime To { get; set; }
        public string? Notes { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
