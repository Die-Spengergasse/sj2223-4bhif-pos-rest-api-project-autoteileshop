using Microsoft.EntityFrameworkCore;
using Spg.OAuth.Model;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Spg.OAuth.Infrastructure
{
    public class AutoTeileShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }
       

        public AutoTeileShopContext(DbContextOptions options) : base(options)
        {
        }

        protected AutoTeileShopContext() : this(new DbContextOptions<DbContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                //options.UseSqlite("Data Source=\\Spg.AutoTeileShop\\src\\Spg.AutoTeileShop.API\\db\\AutoTeileShop.db"); //Home PC
                options.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db"); //Home PC
                                                                                                                                                                 //  D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db"     //Laptop


        }


    }
}
