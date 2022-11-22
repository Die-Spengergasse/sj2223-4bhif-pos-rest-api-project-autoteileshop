using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;s

namespace Spg.AutoTeileShop.Domain.Interfaces
{
    public interface IDeletableProductService
    {
        void Delete(Product product);

    }
}
