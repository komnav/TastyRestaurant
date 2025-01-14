using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public OrdersStatus Status { get; set; }
    }
}
