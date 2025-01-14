using Domain.Enums;

namespace Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public required MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSum { get; set; }
        public OrderDetailStatus Status { get; set; }
    }
}
