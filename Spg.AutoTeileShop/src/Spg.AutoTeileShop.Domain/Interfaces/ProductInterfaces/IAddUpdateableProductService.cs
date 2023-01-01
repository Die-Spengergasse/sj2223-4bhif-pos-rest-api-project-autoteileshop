using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces
{
    public interface IAddUpdateableProductService
    {
        IEnumerable<Product> GetAll();
        Product? Add(Product product);
        Product? Update(Product product);

    }
}
