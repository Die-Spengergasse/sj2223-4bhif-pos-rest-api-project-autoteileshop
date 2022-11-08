using Microsoft.AspNetCore.Mvc;

namespace Spg.AutoTeileShop.MVCFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
