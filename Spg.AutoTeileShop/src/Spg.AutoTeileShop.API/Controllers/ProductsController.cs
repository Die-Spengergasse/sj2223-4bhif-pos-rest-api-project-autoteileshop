using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Reflection.Metadata.Ecma335;

namespace Spg.AutoTeileShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IAddUpdateableProductService _addUpdateproductService;
        private readonly IReadOnlyProductService _readOnlyproductService;
        private readonly IDeletableProductService _deletableProductService;
        private readonly IReadOnlyCatagoryService _readOnlyCatagoryService;

        public ProductsController(IAddUpdateableProductService addUpdateproductService, IReadOnlyProductService readOnlyproductService, IDeletableProductService deletableProductService, IReadOnlyCatagoryService readOnlyCatagoryService)
        {
            _addUpdateproductService = addUpdateproductService;
            _readOnlyproductService = readOnlyproductService;
            _deletableProductService = deletableProductService;
            _readOnlyCatagoryService = readOnlyCatagoryService;
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

        [HttpGet("/filter")]
        public ActionResult<List<ProductDTOFilter>> GetProductByFilterNameorCatagory([FromQuery] string? name, [FromQuery] int catagoryId)
        {
            try
            {
                Catagory? catagory = null;
                if (catagoryId != 0)
                {
                     _readOnlyCatagoryService.GetCatagoryById(catagoryId);
                }
                if ((name is null ||name.Count() == 0 ) && catagory is null) { return BadRequest(); }
                if ((name is null || name.Count() == 0) && catagory is not null)
                {
                    var productsCat = _readOnlyproductService.GetByCatagory(catagory);
                    if (productsCat.Count() == 0) { return NotFound(); }
                    {
                        var ProductCatDTOs = new List<ProductDTOFilter>();
                        foreach (var item in productsCat)
                        {
                            ProductDTOFilter productDTO = new ProductDTOFilter(item);
                            ProductCatDTOs.Add(productDTO);
                        }
                        return Ok(ProductCatDTOs);
                    }
                }
                if ((name.Count() != 0 || name is not null) && catagory is null)
                {
                    List<ProductDTOFilter> productsName = new()
                    {
                        new ProductDTOFilter(_readOnlyproductService.GetByName(name))
                    };
                    if (productsName.Count == 0) return NotFound();
                    return Ok(productsName);

                }
                if ((name.Count() != 0 || name is not null) && catagory is not null)
                {
                    var productsName = _readOnlyproductService.GetByName(name);
                    var ProductsCat = _readOnlyproductService.GetByCatagory(catagory);
                    if (ProductsCat.Count() == 0 || productsName is not null) { return Ok(productsName); }
                    if (ProductsCat.Count() == 0 || productsName is null) { return NotFound(); }
                    if (ProductsCat.Count() != 0 && productsName is not null)
                    {
                        foreach (Product item in ProductsCat)
                        {
                            if (item.Name == name)
                            {
                                return Ok(new ProductDTOFilter(item));
                            }
                        }
                    }
                }
                return NotFound();

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
