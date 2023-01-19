using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class ShoppingCartItemDTOGet
    {
        public int Id { get; private set; }
        public Guid guid { get; set; }
        public int Pieces { get; set; }
        public int? ProductId { get; set; }
        public int? ShoppingCartId { get; set; }

        public ShoppingCartItemDTOGet(ShoppingCartItem cart)
        {
            Id = cart.Id;
            this.guid = cart.guid;
            Pieces = cart.Pieces;
            ProductId = cart.ProductId;
            ShoppingCartId = cart.ShoppingCartId;
        }
    }

    
}
