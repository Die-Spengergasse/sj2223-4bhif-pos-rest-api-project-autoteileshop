using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.MVCFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAddUpdateableProductService _addUpdateproductService;
        private readonly IReadOnlyProductService _readOnlyproductService;


        public ProductController(IAddUpdateableProductService addUpdateproductService, IReadOnlyProductService readOnlyproductService)
        {
            _addUpdateproductService = addUpdateproductService;
            _readOnlyproductService = readOnlyproductService;
        }

        public IActionResult Index()
        {
            List<Product> model = _readOnlyproductService.GetAll().ToList();
            return View(model);
        }
    }
}
