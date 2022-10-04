using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        public int Pieces { get; set; }
        
        public long? ProductId { get; set; }
        public Product? ProductNav { get; set; }
        public long? ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCartNav { get; set; }
    }
}
