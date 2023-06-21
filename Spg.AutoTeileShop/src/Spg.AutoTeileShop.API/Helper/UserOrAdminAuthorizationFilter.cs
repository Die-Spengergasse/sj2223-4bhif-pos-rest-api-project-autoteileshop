using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Helper
{
    public class UserOrAdminAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly UserManager<ClaimsPrincipal> _userManager;

        public UserOrAdminAuthorizationFilter(UserManager<ClaimsPrincipal> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var guid = context.RouteData.Values["guid"];

            string userGuidClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Überprüfung, ob der Benutzer die Guid besitzt oder Admin ist
            if (!guid.Equals(Guid.Parse(userGuidClaim)) && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
