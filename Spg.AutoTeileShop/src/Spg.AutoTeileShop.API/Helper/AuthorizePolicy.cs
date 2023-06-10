using Microsoft.AspNetCore.Authorization;

namespace Spg.AutoTeileShop.API.Helper
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; }

        public CustomAuthorizationRequirement(params string[] roles)
        {
            Roles = roles;
        }
    }


    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated && requirement.Roles.Any(role => context.User.IsInRole(role)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }


}
