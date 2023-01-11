using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetById(int Id);
        ShoppingCart? GetByUserNav(User user);
        IEnumerable<ShoppingCartItem> GetAll();
        ShoppingCart AddShoppingCart(ShoppingCart shoppingCart);
        ShoppingCart Remove(ShoppingCart shoppingCart);
        ShoppingCart UpdateShoppingCart(ShoppingCart item);
        //bool Clear_List(ShoppingCart shoppingCart);
        //bool Add_Item_to_List_or_increas_Pieces_in_Item(ShoppingCart shoppingCart, ShoppingCartItem item);
        //bool RemoveShoppingCartItem(ShoppingCart shoppingCart, ShoppingCartItem item);
        //bool Remove_Item_from_List_or_decrease_Pieces_in_Item(ShoppingCart shoppingCart, ShoppingCartItem item);
    }
}
