using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.OAuth.Infrastructure
{
    public static class DatabaseExtentions
    {
        public static void ConfigureSQLite(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<AutoTeileShopContext>(options =>
            {
                if (!options.IsConfigured)
                    options.UseSqlite(connectionString);
            });

        }
    }
}
