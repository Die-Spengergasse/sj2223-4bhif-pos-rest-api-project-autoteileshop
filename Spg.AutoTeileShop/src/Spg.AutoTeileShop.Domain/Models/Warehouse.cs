using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{

    public class Warehouse
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }

        private List<Product> _products = new();
        public IReadOnlyList<Product> Products => _products;


        public void AddProduct(Product entity)
        {
            if (entity is not null)
                _products.Add(entity);
        }

        public void RemoveProduct(Product entity)
        {
            if (entity is not null)
                _products.Remove(entity);
        }

    }
}
