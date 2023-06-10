namespace Spg.AutoTeileShop.Domain.Models
{
    public class EntityBase
    {
        public int Id { get; private set; } // PK
        public DateTime? LastChangeDate { get; set; }
    }
}
