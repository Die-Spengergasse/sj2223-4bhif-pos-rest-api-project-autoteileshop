using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Spengershop.Domain.Model
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product? ProductNav { get; set; }
        public ShoppingCart? ShoppingCartNav { get; set; }
    }
}
