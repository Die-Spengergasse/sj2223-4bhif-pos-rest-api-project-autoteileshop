using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; private set; }
        public Guid guid { get;  set; }
        public int Pieces { get; set; }        
        public int? ProductId { get; set; }
        public Product? ProductNav { get; set; }
        public int? ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCartNav { get; set; }

        public ShoppingCartItem()
        {
        }

        public ShoppingCartItem(int id, Guid guid, int pieces, int? productId, Product productNav, int? shoppingCartId, ShoppingCart? shoppingCartNav)
        {
            Id = id;
            this.guid = guid;
            Pieces = pieces;
            ProductId = productId;
            ProductNav = productNav;
            ShoppingCartId = shoppingCartId;
            ShoppingCartNav = shoppingCartNav;
        }

        public ShoppingCartItem(Guid guid, int pieces, int? productId, Product productNav, int? shoppingCartId, ShoppingCart? shoppingCartNav)
        {
            this.guid = guid;
            Pieces = pieces;
            ProductId = productId;
            ProductNav = productNav;
            ShoppingCartId = shoppingCartId;
            ShoppingCartNav = shoppingCartNav;
        }
        public ShoppingCartItem(ShoppingCartItemPostDTO dto)
        {
            guid = Guid.NewGuid();
            Pieces = dto.Pieces;
            ProductNav = dto.ProductNav;
            ShoppingCartNav = dto.ShoppingCartNav;
        }
    }
}
