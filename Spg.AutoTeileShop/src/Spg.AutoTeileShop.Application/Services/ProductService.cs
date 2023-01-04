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

        public Product? Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product? Delete(Product product)
        {
            return _productRepository.Delete(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetByCatagory(Catagory catagory)
        {
            return _productRepository.GetByCatagory(catagory);
        }

        public Product? GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product? GetByName(string name)
        {
            return _productRepository.GetByName(name);
        }

        public Product? Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
