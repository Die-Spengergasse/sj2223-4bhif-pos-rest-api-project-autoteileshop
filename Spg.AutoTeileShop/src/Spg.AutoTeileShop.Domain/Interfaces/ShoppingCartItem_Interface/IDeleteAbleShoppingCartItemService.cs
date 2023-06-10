using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IDeleteAbleShoppingCartItemService
    {
        ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem);
    }
}
