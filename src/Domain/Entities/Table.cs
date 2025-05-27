using Domain.Enums;

namespace Domain.Entities
{
    public class  Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public TableType Type { get; set; }
    }
}
