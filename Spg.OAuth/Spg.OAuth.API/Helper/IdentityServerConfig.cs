using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Spg.OAuth.API.Helper
{
    public class IdentityServerConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes);
                //.AddTestUsers(Config.Users)
                //.AddAspNetIdentity<ApplicationUser>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }
    }

}
