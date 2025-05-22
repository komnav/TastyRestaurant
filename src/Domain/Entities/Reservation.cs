﻿using Domain.Enums;

namespace Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public Table? Table { get; set; }
        public int UserId { get; set; }
        public User? Customer { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Notes { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
