using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class ShoppingCartItemPostDTO
    {
        public int Pieces { get; set; }
        public Product? ProductNav { get; set; }
        public ShoppingCart? ShoppingCartNav { get; set; }
    }
}
