using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public OrdersStatus Status { get; set; }
    }
}