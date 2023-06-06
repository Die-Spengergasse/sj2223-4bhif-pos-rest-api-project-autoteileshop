using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCartItem_Interface;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IAddUpdateableShoppingCartItemService _addUpdateableShoppingCartItemService;
        private readonly IReadOnlyShoppingCartItemService _readOnlyShoppingCartItemService;
        private readonly IDeleteAbleShoppingCartItemService _deleteAbleShoppingCartItemService;
  

        public ShoppingCartItemController(IAddUpdateableShoppingCartItemService addUpdateableShoppingCartItemService, IReadOnlyShoppingCartItemService readOnlyShoppingCartItemService, IDeleteAbleShoppingCartItemService deleteAbleShoppingCartItemService)
        {
            _addUpdateableShoppingCartItemService = addUpdateableShoppingCartItemService;
            _readOnlyShoppingCartItemService = readOnlyShoppingCartItemService;
            _deleteAbleShoppingCartItemService = deleteAbleShoppingCartItemService;
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<List<ShoppingCartItem>> Add()
        {
            try
            {
                List<ShoppingCartItem> requestBody = _addUpdateableShoppingCartItemService.Add().ToList(); //Hier fehlt die DTO

                if (requestBody.Count == 0) { return NotFound(); }
                return Ok(requestBody);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); // Test halber wird hier die Exception zurückgegeben
            }
        }

        [HttpPut("")]
        [Produces("application/json")]
        public ActionResult<ShoppingCartItem> Delete(ShoppingCartItemDTO pDto)
        {
            try
            {
                return _deleteAbleShoppingCartItemService.Delete(new ShoppingCartItem(pDto)); //Hier fehlt DTO
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetByGuid/{Guid}")]
        public ActionResult<List<ShoppingCartItem>> GetByGuid(Guid Guid)
        {
            try
            {
                List<Product> products = _readOnlyShoppingCartItemService.GetByGuid(Guid).ToList();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e); // Test halber wird hier die Exception zurückgegeben
            }
        }

    }
}
