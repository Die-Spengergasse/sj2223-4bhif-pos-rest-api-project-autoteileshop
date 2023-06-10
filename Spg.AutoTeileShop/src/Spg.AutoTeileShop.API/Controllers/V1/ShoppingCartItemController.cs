using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IDeleteAbleShoppingCartItemService _deleteAbleShoppingCartItemService;
        private readonly IAddUpdateableShoppingCartItemService _addUpdateableShoppingCartItemService;
        private readonly IReadOnlyShoppingCartItemService _readOnlyShoppingCartItemService;
        private readonly IReadOnlyShoppingCartService _readOnlyShoppingCartService;

        public ShoppingCartItemController(IDeleteAbleShoppingCartItemService deleteAbleShoppingCartItemService, IAddUpdateableShoppingCartItemService addUpdateableShoppingCartItemService, IReadOnlyShoppingCartItemService readOnlyShoppingCartItemService, IReadOnlyShoppingCartService readOnlyShoppingCartService)
        {
            _deleteAbleShoppingCartItemService = deleteAbleShoppingCartItemService;
            _addUpdateableShoppingCartItemService = addUpdateableShoppingCartItemService;
            _readOnlyShoppingCartItemService = readOnlyShoppingCartItemService;
            _readOnlyShoppingCartService = readOnlyShoppingCartService;
        }

        // All - Authorization

        [HttpGet("")]
        public ActionResult<List<ShoppingCartItem>> GetAllShoppingCartItems()
        {
            var items = _readOnlyShoppingCartItemService.GetAll();
            if (items.Count() == 0 || items is null)
                return NotFound();
            return Ok(items);
        }

        [HttpGet("{guid}")]
        public ActionResult<ShoppingCartItem> GetShoppingCartItemByGuid(Guid guid)
        {
            try
            {
                var item = _readOnlyShoppingCartItemService.GetByGuid(guid);
                if (item is null)
                    return NotFound();
                return Ok(item);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ShoppingCart")]
        public ActionResult<List<ShoppingCartItemDTOGet>> GetShoppingCartItemByShoppingCart([FromQuery] int shoppingCartId)
        {
            try
            {
                if (shoppingCartId == 0) return BadRequest();
                var shoppingCart = _readOnlyShoppingCartService.GetById(shoppingCartId);
                var items = _readOnlyShoppingCartItemService.GetByShoppingCart(shoppingCart);
                if (items.Count() == 0 || items is null)
                    return NotFound();

                List<ShoppingCartItemDTOGet> itemsDTO = new();
                foreach (ShoppingCartItem item in items)
                {
                    itemsDTO.Add(new ShoppingCartItemDTOGet(item));
                }
                return Ok(itemsDTO);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<ShoppingCartItem> AddShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                var item = _addUpdateableShoppingCartItemService.Add(shoppingCartItem);
                return CreatedAtAction(nameof(GetShoppingCartItemByGuid), new { item.guid }, item);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        [Produces("application/json")]
        public ActionResult<ShoppingCartItem> UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                var item = _addUpdateableShoppingCartItemService.Update(shoppingCartItem);
                return Ok(item);
            }
            catch (KeyNotFoundException kE) { return BadRequest(kE.Message); }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("")]
        public ActionResult<ShoppingCartItem> DeleteShoppingCartItem(Guid guid)
        {
            try
            {
                var item = _deleteAbleShoppingCartItemService.Delete(_readOnlyShoppingCartItemService.GetByGuid(guid));
                return Ok(item);
            }
            catch (KeyNotFoundException kE) { return Ok(); }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
