using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces
{
    public interface IReadOnlyProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetByName(string name);
        Product? GetById(int id);

    }
}
