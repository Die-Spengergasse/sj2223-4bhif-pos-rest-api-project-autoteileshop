﻿using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface
{
    public interface IDeleteAbleShoppingCartItemService
    {
        ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem);
    }
}