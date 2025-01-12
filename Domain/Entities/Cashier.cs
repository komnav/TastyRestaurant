namespace Domain.Entities
{
    public class Cashier
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
