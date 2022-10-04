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

        public bool AddShoppingCartItem(ShoppingCartItem entity)
        {
            if (entity is not null) // Kann garnicht null sein
            {
                if (entity.Pieces <= entity.ProductNav.Stock)
                {
                    entity.ProductNav.Stock = entity.ProductNav.Stock - entity.Pieces;
                    try
                    {
                        ShoppingCartItem? exsitingShoppingCartItem = _shoppingCartItems.SingleOrDefault(s => s.ProductNav.Guid == entity.ProductNav.Guid);
                        if (exsitingShoppingCartItem is not null)
                        {

                            exsitingShoppingCartItem.Pieces = exsitingShoppingCartItem.Pieces + entity.Pieces; //richtig gestellt
                            return true;
                        }
                        else
                        {
                            _shoppingCartItems.Add(entity);
                            return true;
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e);
                        //throw;
                        return false;
                    }
                }
                else
                {
                    throw new Exception("Not enough stock");
                    //return false;
                }
            }
            return false;
        }

        public void RemoveShoppingCartItem(ShoppingCartItem entity)
        {
            if (entity is not null)
                _shoppingCartItems.Remove(entity);
        }
    }
}

