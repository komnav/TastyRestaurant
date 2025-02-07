namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
