using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces
{
    public interface IProductRepositroy
    {
        public IEnumerable<Product> GetAll();
        public Product? GetById(int Id);
        public Product? GetByName(string name);
        public Product? Delete(Product product);
        public Product? Add(Product product);
        public Product? Update(Product product);
    }
}
