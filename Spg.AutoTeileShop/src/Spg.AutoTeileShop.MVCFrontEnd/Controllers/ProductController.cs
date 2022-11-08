using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.MVCFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            List<Product> model = _productService.GetAll().ToList();
            return View(model);
        }
    }
}
