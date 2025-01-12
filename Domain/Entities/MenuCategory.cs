namespace Domain.Entities
{
    public class MenuCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
