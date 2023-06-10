using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetById(int Id);
        ShoppingCart? GetByUserNav(Guid userGuid);
        IEnumerable<ShoppingCart> GetAll_includeItems();
        ShoppingCart AddShoppingCart(ShoppingCart shoppingCart);
        ShoppingCart Remove(ShoppingCart shoppingCart);
        ShoppingCart UpdateShoppingCart(ShoppingCart item);
        ShoppingCart GetByGuid(Guid guid);
        //bool Clear_List(ShoppingCart shoppingCart);
        //bool Add_Item_to_List_or_increas_Pieces_in_Item(ShoppingCart shoppingCart, ShoppingCartItem item);
        //bool RemoveShoppingCartItem(ShoppingCart shoppingCart, ShoppingCartItem item);
        //bool Remove_Item_from_List_or_decrease_Pieces_in_Item(ShoppingCart shoppingCart, ShoppingCartItem item);
    }
}
