using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class ProductService : IAddUpdateableProductService, IReadOnlyProductService, IDeletableProductService
    {
        private readonly IProductRepositroy _productRepository;
        public ProductService(IProductRepositroy productRepository)
        {
            _productRepository = productRepository;
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product? GetByName(string name)
        {
            return _productRepository.GetByName(name);
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
