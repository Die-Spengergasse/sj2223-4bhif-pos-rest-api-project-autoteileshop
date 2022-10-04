using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class ShoppingCart
    {
        public long Id { get; set; }
        public Guid guid { get; set; }
        private List<ShoppingCartItem> _shoppingCartItems = new();

        public IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;
        
        public virtual int CustomerId { get; set; }
        public virtual Customer? CustomerNav { get; set; }
        
        
        public ShoppingCart(int id, Guid guid)
        {
            Id = id;
            this.guid = guid;
        }

        public ShoppingCart()
        {
        }

        public void AddShoppingCartItem(ShoppingCartItem entity)
        {
            if (entity is not null)
            {
                if (entity.Pieces <= entity.ProductNav.Stock)
                {
                    entity.ProductNav.Stock = entity.ProductNav.Stock - entity.Pieces;
                    try
                    {
                        //ShoppingCartItem? exsitingShoppingCartItem = _shoppingCartItems.SingleOrDefault(s => s.ProductNav.Id == entity.ProductNav.Id);
                        ShoppingCartItem? exsitingShoppingCartItem = _shoppingCartItems.SingleOrDefault(s => s.ProductNav.Guid == entity.ProductNav.Guid);
                        if (exsitingShoppingCartItem is not null)
                        {

                            exsitingShoppingCartItem.Pieces = entity.Pieces + entity.Pieces;
                        }
                        else
                        {
                            _shoppingCartItems.Add(entity);
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e);
                        //throw;
                    }
                }
                else
                {
                    //throw new Exception("Not enough stock");
                }
            }
        }

        public void RemoveShoppingCartItem(ShoppingCartItem entity)
        {
            if (entity is not null)
                _shoppingCartItems.Remove(entity);
        }
    }
}

