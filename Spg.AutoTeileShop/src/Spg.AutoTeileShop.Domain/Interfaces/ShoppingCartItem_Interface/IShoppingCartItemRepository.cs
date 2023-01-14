using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IShoppingCartItemRepository
    {
        ShoppingCartItem GetById(int Id);
        ShoppingCartItem GetByGuid(Guid guid);
        ShoppingCartItem Update(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Add(ShoppingCartItem shoppingCartItem);
        ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem);

    }
}
