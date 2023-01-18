﻿using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IReadOnlyShoppingCartItemService
    {
        IEnumerable<ShoppingCartItem> GetAll();
        ShoppingCartItem GetByGuid(Guid guid);
        ShoppingCartItem GetById(int Id);
        IEnumerable<ShoppingCartItem> GetByShoppingCart(ShoppingCart shoppingCart);
    }
}