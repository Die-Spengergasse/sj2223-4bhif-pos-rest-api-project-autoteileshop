using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Spg.AutoTeileShop.Infrastructure;
using System.Threading.Tasks;

namespace Spg.OAuth.API.Helper
{
    public class ProfileService : IProfileService
    {
        protected readonly AutoTeileShopContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public IProfileService( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
