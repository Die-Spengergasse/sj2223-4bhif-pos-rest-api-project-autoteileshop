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
    public class ShoppingCart : EntityBase
    {
        public int Id { get; private set; }
        public Guid guid { get;  set; }        
        public int? UserId { get; set; }
        public User? UserNav { get; set; }
        private List<ShoppingCartItem> _shoppingCartItems = new();
        public IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;


        public ShoppingCart(int id, Guid guid)
        {
            Id = id;
            this.guid = guid;
        }

        public ShoppingCart()
        {
        }

        public ShoppingCart(int id, Guid guid, int UserId, User? UserNav, List<ShoppingCartItem> shoppingCartItems) : this(id, guid)
        {
            this.UserId = UserId;
            this.UserNav = UserNav;
            _shoppingCartItems = shoppingCartItems;
        }

        public ShoppingCart(ShoppingCartPostDTO scPDto)
        {
            UserId = scPDto.UserId;
            _shoppingCartItems.AddRange(scPDto.ShoppingCartItems.ToList());
        }

        public bool AddShoppingCartItem(ShoppingCartItem item)
        {
            if (item is not null) // Kann garnicht null sein
            {
                if (item.Pieces <= item.ProductNav.Stock)
                {
                    return Add_Item_to_List_or_increas_Pieces_in_Item(item);
                }
                else
                {
                    throw new Exception("Not enough stock");
                    //return false;
                }
            }
            return false;
        }
        public bool Add_Item_to_List_or_increas_Pieces_in_Item(ShoppingCartItem item)
        {
            try
            {
                ShoppingCartItem? exsitingShoppingCartItem = _shoppingCartItems.SingleOrDefault(s => s.ProductNav.Guid == item.ProductNav.Guid);
                if (exsitingShoppingCartItem is not null)
                {

                    exsitingShoppingCartItem.Pieces += item.Pieces;
                    item.ProductNav.Stock = item.ProductNav.Stock - item.Pieces;
                    return true;

                }
                else
                {
                    _shoppingCartItems.Add(item);
                    item.ProductNav.Stock -= item.Pieces;
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
        
        public void RemoveShoppingCartItem(ShoppingCartItem item)
        {
            if (item is not null)
            {
                if (_shoppingCartItems.SingleOrDefault(s => s.guid == item.guid) != null)
                {
                    ShoppingCartItem? shc = _shoppingCartItems.Single(s => s.guid == item.guid);
                    if (shc.Pieces - item.Pieces < 1)
                    {
                        _shoppingCartItems.Remove(item);
                    }
                    else
                    {
                        _shoppingCartItems.Single(s => s.guid == item.guid).Pieces -= item.Pieces;
                    }
                    
                }
                else { throw new Exception("ShoppingCart does not contains this ShoppingCarItem"); }
            }
        }

        
    }
}

