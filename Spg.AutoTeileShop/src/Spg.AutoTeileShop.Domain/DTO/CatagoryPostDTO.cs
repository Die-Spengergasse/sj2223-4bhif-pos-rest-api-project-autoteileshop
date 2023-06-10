using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class CatagoryPostDTO
    {
        public CatagoryPostDTO()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryTypes CategoryType { get; set; }
        public int TopCatagoryId { get; set; }

    }

}
