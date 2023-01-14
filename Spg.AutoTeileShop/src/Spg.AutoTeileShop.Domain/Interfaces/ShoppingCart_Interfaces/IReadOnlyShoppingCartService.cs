using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces
{
    public interface IReadOnlyShoppingCartService
    {
        ShoppingCart GetById(int Id);
        ShoppingCart GetByGuid(Guid guid);
        ShoppingCart? GetByUserNav(Guid userGuid);
        IEnumerable<ShoppingCartItem> GetAll();
    }
}
