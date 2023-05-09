using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO_MVC;

namespace Spg.AutoTeileShop.MVCFrontEnd.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            string userName = HttpContext.Request.Cookies["User"] ?? "not loged in";
            return View("Login", userName);
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            HttpContext.Response.Cookies.Append("User", dto.Username);
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
