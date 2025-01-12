namespace Domain.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
