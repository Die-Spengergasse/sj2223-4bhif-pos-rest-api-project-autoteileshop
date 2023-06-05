using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class ShoppingCartItemPostDTO
    {
        public int Pieces { get; set; }
        public Product? ProductNav { get; set; }
        public ShoppingCart? ShoppingCartNav { get; set; }
    }
}
