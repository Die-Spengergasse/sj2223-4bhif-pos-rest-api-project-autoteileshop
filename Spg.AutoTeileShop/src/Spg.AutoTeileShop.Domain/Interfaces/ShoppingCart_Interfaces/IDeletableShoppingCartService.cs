using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IDeletableShoppingCartService
    {
        ShoppingCart Remove(ShoppingCart shoppingCart);
    }
}
