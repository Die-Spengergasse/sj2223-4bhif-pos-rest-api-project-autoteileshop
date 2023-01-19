using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
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
        public ActionResult<List<Product>> GetAllProduct()
        {
            try
            {
                List<Product> requestBody = _readOnlyproductService.GetAll().ToList();
                
                if (requestBody.Count == 0) { return NotFound(); }
                return Ok(requestBody);
            }
            catch (Exception e)
            {
                return BadRequest(); // Test halber wird hier die Exception zurückgegeben
            }
        }

        [HttpGet("/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                Product? product = _readOnlyproductService.GetById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException kE)
            {
                return NotFound(kE.Message);
            }
            catch (Exception e)
            {
                return BadRequest(); // Test halber wird hier die Exception zurückgegeben
            }
        }

        

        [HttpGet("/ByName/{name}")]
        public ActionResult<Product> GetProductByName(string name)
        {
            try
            {
                Product? product = _readOnlyproductService.GetByName(name);
                return Ok(product);
            }
            catch (KeyNotFoundException kE)
            {
                return NotFound(kE.Message);
            }
            catch (Exception e)
            {
                return BadRequest(); 
            }
        }

        [HttpGet("/ByCatagory")]
        public ActionResult<List<Product>> GetProductByCatagory([FromQuery] Catagory catagory)
        {
            try
            {
                List<Product> products = _readOnlyproductService.GetByCatagory(catagory).ToList();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<Product> AddProduct(ProductDTO pDto)
        {
            try
            {
                var product = _addUpdateproductService.Add(new Product(pDto));
                return Created("/api/Product/" + product.Guid, product);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        [Produces("application/json")]
        public ActionResult<Product> UpdateProduct(ProductDTO pDto)
        {
            try
            {
                return _addUpdateproductService.Update(new Product(pDto));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
