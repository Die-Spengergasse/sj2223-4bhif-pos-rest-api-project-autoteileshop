using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
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

        [HttpGet("")]
        public IActionResult GetAllProduct()
        {
            try
            {
                List<Product> requestBody = _readOnlyproductService.GetAll().ToList();
                //List<ProductDTO> requestBodyDTO = new List<ProductDTO>();
                //foreach(Product p in requestBody)
                //{
                //    ProductDTO pDto = new ProductDTO(p);
                //    requestBodyDTO.Add(pDto);
                //}
                if (requestBody.Count == 0) { return NotFound(); }
                return Ok(requestBody);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Test halber wird hier die Exception zurückgegeben
            }
        }
    }
}
