using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.MVCFrontEnd.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Helpers;

namespace Spg.AutoTeileShop.MVCFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string? userInfoJson = HttpContext.Request.Cookies["4bhif_login"];
            if (string.IsNullOrEmpty(userInfoJson))
            {
                return RedirectToAction("Unauthorized", "Home");
            }
            UserUpdateDTO userInfo = JsonSerializer.Deserialize<UserUpdateDTO>(HttpContext.Request.Cookies["4bhif_login"]);
            if (userInfo is not null)
            {
                return RedirectToAction("Unauthorized", "Home");
            }
            return View("Privacy" , userInfoJson);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
