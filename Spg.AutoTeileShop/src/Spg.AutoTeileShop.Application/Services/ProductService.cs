using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositroy _productRepository;
        public ProductService(IProductRepositroy productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetByName(string name)
        {
            return _productRepository.GetByName(name);
        }

    }
}
