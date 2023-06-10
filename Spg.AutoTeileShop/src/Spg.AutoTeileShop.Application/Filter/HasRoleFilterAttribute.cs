using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Spg.AutoTeileShop.Application.Filter
{
    public class HasRoleFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string role = context.HttpContext.Request.Headers["role"].ToString() ?? string.Empty;
            if (role != "admin")
            {
                context.Result = new UnauthorizedObjectResult("Zugriff verweigert");
            }
        }
    }
}
