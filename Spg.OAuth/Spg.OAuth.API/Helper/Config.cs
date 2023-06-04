using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Spg.OAuth.API.Helper
{
    public static class Config
    {
        static List<User> users = createDB().Users.ToList();
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

        private static AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite(@"Data Source = I:\Dokumente 4TB\HTL\4 Klasse\POS1 Git Repo\sj2223-4bhif-pos-rest-api-project-autoteileshop\Spg.AutoTeileShop\src\Spg.AutoTeileShop.API\db\AutoTeileShop.db")     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
    }

}
