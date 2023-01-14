using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class ShoppingCartItemService : IDeleteAbleShoppingCartItemService, IAddUpdateableShoppingCartItemService, IReadOnlyShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        public ShoppingCartItemService(IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public ShoppingCartItem Add(ShoppingCartItem shoppingCartItem)
        {
            return _shoppingCartItemRepository.Add(shoppingCartItem);
        }

        public ShoppingCartItem Delete(ShoppingCartItem shoppingCartItem)
        {
            return _shoppingCartItemRepository.Delete(shoppingCartItem);
        }

        public IEnumerable<ShoppingCartItem> GetAll()
        {
            return _shoppingCartItemRepository.GetAll();
        }

        public ShoppingCartItem GetByGuid(Guid guid)
        {
            return _shoppingCartItemRepository.GetByGuid(guid);
        }

        public ShoppingCartItem GetById(int Id)
        {
            return _shoppingCartItemRepository.GetById(Id);
        }

        public IEnumerable<ShoppingCartItem> GetByShoppingCart(ShoppingCart shoppingCart)
        {
            var items = _shoppingCartItemRepository.GetAllIncludeShoppingCartNav();
            return items.Where(s => s.ShoppingCartNav == shoppingCart);

        }

        public ShoppingCartItem Update(ShoppingCartItem shoppingCartItem)
        {
            var sCI = _shoppingCartItemRepository.GetByGuid(shoppingCartItem.guid) ?? throw new KeyNotFoundException($"No Item found with guid: {shoppingCartItem.guid}");
            sCI.guid = shoppingCartItem.guid;
            sCI.Pieces = shoppingCartItem.Pieces;
            sCI.ShoppingCartId = shoppingCartItem.ShoppingCartId;
            sCI.ShoppingCartNav = shoppingCartItem.ShoppingCartNav;
            sCI.ProductNav = shoppingCartItem.ProductNav;
            sCI.ProductId = sCI.ProductId;

            return _shoppingCartItemRepository.Update(sCI);

        }
        
    }
}
