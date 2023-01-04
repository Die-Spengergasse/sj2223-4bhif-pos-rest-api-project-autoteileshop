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

        public int GetDiscountById(int id)
        {
            return _productRepository.GetById(id).Discount;
        }

        public string GetEan13ById(int id)
        {
            return _productRepository.GetById(id).Ean13;
        }

        public string GetImageById(int id)
        {
            return _productRepository.GetById(id).Image;
        }

        public decimal GetPriceById(int id)
        {
            return _productRepository.GetById(id).Price;
        }

        public DateTime GetReceiveById(int id)
        {
            return _productRepository.GetById(id).receive;
        }

        public int GetStockById(int id)
        {
            return _productRepository.GetById(id).Stock;
        }

        public Product? Update(Product product)
        {
            var product1 = _productRepository.GetById(product.Id);
            product1.Name = product.Name;
            product1.Price = product.Price;
            product1.Stock = product.Stock;
            product1.receive = product.receive;
            product1.Discount = product.Discount;
            product1.Ean13 = product.Ean13;
            product1.Image = product.Image;
            product1.catagory = product.catagory;
            
            return _productRepository.Update(product1);
        }
    }
}
