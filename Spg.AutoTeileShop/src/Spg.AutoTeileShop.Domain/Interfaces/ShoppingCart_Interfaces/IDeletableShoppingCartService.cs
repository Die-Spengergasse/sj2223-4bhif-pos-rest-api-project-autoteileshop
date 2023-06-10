using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IDeletableShoppingCartService
    {
        ShoppingCart Remove(ShoppingCart shoppingCart);
    }
}
