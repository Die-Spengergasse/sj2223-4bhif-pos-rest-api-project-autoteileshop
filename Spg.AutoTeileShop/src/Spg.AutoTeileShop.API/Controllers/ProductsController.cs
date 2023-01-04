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
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Test halber wird hier die Exception zurückgegeben
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                Product? product = _readOnlyproductService.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex); // Test halber wird hier die Exception zurückgegeben
            }
        }

        [HttpGet("GetByName/{name}")]
        public ActionResult<Product> GetProductByName(string name)
        {
            try
            {
                Product? product = _readOnlyproductService.GetByName(name);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex); // Test halber wird hier die Exception zurückgegeben
            }
        }

        [HttpGet("GetByCatagory/{catagory}")]
        public ActionResult<List<Product>> GetProductByCatagory(Catagory catagory)
        {
            try
            {
                List<Product> products = _readOnlyproductService.GetByCatagory(catagory).ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex); // Test halber wird hier die Exception zurückgegeben
            }
        }


    }
}
