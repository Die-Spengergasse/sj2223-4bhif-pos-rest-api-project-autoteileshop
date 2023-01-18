﻿using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly AutoTeileShopContext _db;

        public ShoppingCartItemRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public ShoppingCartItem Add(ShoppingCartItem shoppingCartItem)
        {
           _db.ShoppingCartItems.Add(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Remove(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public IEnumerable<ShoppingCartItem> GetAll()
        {
            return _db.ShoppingCartItems.ToList();
        }

        public ShoppingCartItem GetByGuid(Guid guid)
        {
            return (ShoppingCartItem)(_db.ShoppingCartItems.Where(s => s.guid == guid) ?? throw new KeyNotFoundException($"No Item found with Guid {guid}"));
        }

        public ShoppingCartItem GetById(int Id)
        {
            return _db.ShoppingCartItems.Find(Id) ?? throw new KeyNotFoundException($"No Item found with Guid {Id}");
        }

        public ShoppingCartItem Update(ShoppingCartItem shoppingCartItem)
        {
            _db.ShoppingCartItems.Update(shoppingCartItem);
            _db.SaveChanges();
            return shoppingCartItem;
        }

        public IEnumerable<ShoppingCartItem> GetAllIncludeShoppingCartNav()
        {
            return _db.ShoppingCartItems.Include(s => s.ShoppingCartNav).ToList();
        }
    }
}