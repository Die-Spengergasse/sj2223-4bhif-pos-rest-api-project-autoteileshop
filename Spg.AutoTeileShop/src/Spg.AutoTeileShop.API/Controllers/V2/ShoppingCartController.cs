using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.ShoppingCart_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IReadOnlyShoppingCartService _redOnlyShoppingCartService;
        private readonly IDeletableShoppingCartService _deletableShoppingCartService;
        private readonly IAddUpdateableShoppingCartService _addUpdatableShoppingCartService;
        private readonly IReadOnlyUserService _readOnlyUserService;

        //Hateaos
        private readonly IEnumerable<EndpointDataSource> _endpointSources;
        private List<BuildRoutePattern> _routes;

        public ShoppingCartController
        (IReadOnlyShoppingCartService shoppingCartService, IDeletableShoppingCartService deletableShoppingCartService,
        IAddUpdateableShoppingCartService updatableShoppingCartService, IReadOnlyUserService readOnlyUserService, IEnumerable<EndpointDataSource> endpointSources,
        ListAllEndpoints listAllEndpoints)

        {
            _redOnlyShoppingCartService = shoppingCartService;
            _deletableShoppingCartService = deletableShoppingCartService;
            _addUpdatableShoppingCartService = updatableShoppingCartService;
            _readOnlyUserService = readOnlyUserService;
            
            //Hateaos
            _endpointSources = endpointSources;
            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));
            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);

        }

        [HttpGet("")]
        public ActionResult<List<ShoppingCart>> GetAllShoppingCarts()
        {
            try
            {

                List<ShoppingCart> carts = _redOnlyShoppingCartService.GetAll().ToList();
                HateoasBuild<ShoppingCart, int> hb = new HateoasBuild<ShoppingCart, int>();

                if (carts.Count == 0) { return NotFound(); }
                return Ok(hb.buildHateoas(carts.ToList(), carts.Select(s => s.Id).ToList(), _routes));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{guid}")]
        public ActionResult<ShoppingCart> GetShoppingCartByGuid(Guid guid)
        {
            try
            {
                var cart = _redOnlyShoppingCartService.GetByGuid(guid);
                HateoasBuild<ShoppingCart, int> hb = new HateoasBuild<ShoppingCart, int>();

                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(hb.buildHateoas(cart, cart.Id, _routes));
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

        [HttpGet("ByUser")]
        public ActionResult<ShoppingCart> GetShoppingCartByUserNav([FromQuery] Guid userGuid)
        {
            try
            {

                var cart = _redOnlyShoppingCartService.GetByUserNav(userGuid);
                HateoasBuild<ShoppingCart, int> hb = new HateoasBuild<ShoppingCart, int>();

                if (cart == null)
                {
                    return NotFound();
                }
                return Ok(hb.buildHateoas(cart, cart.Id, _routes));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No Cart found with UserNav: " + userGuid)) return BadRequest(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<ShoppingCart> AddShoppingCart(ShoppingCartPostDTO cartDTO)
        {
            try
            {
                ShoppingCart cart = new(cartDTO);
                if (cartDTO is null) return BadRequest("User Navigation is null");
                cart.UserNav = _readOnlyUserService.GetById((int)cartDTO.UserId);
                var newCart = _addUpdatableShoppingCartService.AddShoppingCart(cart);
                return Created($"/api/ShoppingCart/{newCart.guid}", newCart);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains($"No User Found with Id: {cartDTO.UserId}")) return BadRequest("User Navigation: " + ex.Message);
                return BadRequest();
            }
        }

    }
}
