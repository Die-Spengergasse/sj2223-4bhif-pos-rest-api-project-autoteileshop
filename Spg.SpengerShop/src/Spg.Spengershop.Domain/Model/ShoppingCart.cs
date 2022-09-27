using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Spengershop.Domain.Model
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        private List<ShoppingCartItem> _shoppingCartItems = new();

        

        public IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;
        public int CustomerId { get; set; }
        public Customer? CustomerNav { get; set; }
        public ShoppingCart(int id, Guid guid)
        {
            Id = id;
            this.guid = guid;
        }

        public void AddShoppingCartItem(ShoppingCartItem entity)
        {
            if(entity is not null)
                _shoppingCartItems.Add(entity);
        }

        public void RemoveShoppingCartItem(ShoppingCartItem entity)
        {
            if (entity is not null)
                _shoppingCartItems.Remove(entity);
        }

    }
}
