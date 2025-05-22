using Domain.Enums;

namespace Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderDetailStatus Status { get; set; }
    }
}
