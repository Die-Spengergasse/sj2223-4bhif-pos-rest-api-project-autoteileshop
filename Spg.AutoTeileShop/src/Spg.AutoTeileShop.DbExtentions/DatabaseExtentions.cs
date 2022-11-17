using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.DbExtentions
{
    public static class DatabaseExtentions
    {
        public static void ConfigureSQLite(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<AutoTeileShopContext>(options =>
            {
            if(!options.IsConfigured)
            options.UseSqlite(connectionString));
             }
                
        }
    }
}