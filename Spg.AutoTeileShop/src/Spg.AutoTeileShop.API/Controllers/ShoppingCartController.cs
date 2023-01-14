using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IReadOnlyShoppingCartService _shoppingCartService;
        private readonly IDeletableShoppingCartService _deletableShoppingCartService;
        private readonly IAddUpdateableShoppingCartService _updatableShoppingCartService;

        public ShoppingCartController(IReadOnlyShoppingCartService shoppingCartService, IDeletableShoppingCartService deletableShoppingCartService, IAddUpdateableShoppingCartService updatableShoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
            _deletableShoppingCartService = deletableShoppingCartService;
            _updatableShoppingCartService = updatableShoppingCartService;
        }

        [HttpGet("")]
        public ActionResult<List<ShoppingCart>> GetAll()
        {
            var carts = _shoppingCartService.GetAll();
            if (carts.Count() == 0 || carts == null)
            {
                return NotFound();
            }
            return Ok(carts);
        }


        [HttpGet("{guid}")]
        public ActionResult<ShoppingCart> GetByGuid(Guid guid)
        {
            try
            {
                var cart = _shoppingCartService.GetByGuid(guid);
                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }  

    }
}
