using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Spg.OAuth.API.Helper
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            new Client
            {
                ClientId = "your_client_id",
                ClientSecrets = { new Secret("your_client_secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "api1" }
            }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("api1", "API 1")
            };

        public static IEnumerable<TestUser> Users =>
            new List<TestUser>
            {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password"
            }
            };
    }

}
