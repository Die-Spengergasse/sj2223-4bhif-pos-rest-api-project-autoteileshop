using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.API.DTO;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IAddUpdateableProductService _addUpdateproductService;
        private readonly IReadOnlyProductService _readOnlyproductService;
        private readonly IDeletableProductService _deletableProductService;

        public ProductsController(IAddUpdateableProductService addUpdateproductService, IReadOnlyProductService readOnlyproductService, IDeletableProductService deletableProductService)
        {
            _addUpdateproductService = addUpdateproductService;
            _readOnlyproductService = readOnlyproductService;
            _deletableProductService = deletableProductService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAllProduct()
        {
            try
            {
                IEnumerable<Product> requestBody = _readOnlyproductService.GetAll();
                IEnumerable<Product> requestBodyDTO = (IEnumerable<Product>)requestBody.Select(p => new ProductDTO(p));
                if (requestBodyDTO == null) { return NotFound(); }
                return Ok(requestBodyDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
