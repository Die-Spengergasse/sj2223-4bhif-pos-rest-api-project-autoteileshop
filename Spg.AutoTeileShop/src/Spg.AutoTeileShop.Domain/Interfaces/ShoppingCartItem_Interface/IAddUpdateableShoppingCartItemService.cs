using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IAddUpdateableShoppingCartItemService
    {
        ShoppingCartItem Update(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Add(ShoppingCartItem shoppingCartItem);
    }
}
