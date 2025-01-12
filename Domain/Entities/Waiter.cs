namespace Domain.Entities
{
    public class Waiter
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
