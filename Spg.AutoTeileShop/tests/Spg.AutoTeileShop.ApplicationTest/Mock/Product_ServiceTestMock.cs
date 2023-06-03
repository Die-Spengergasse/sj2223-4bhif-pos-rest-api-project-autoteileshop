using Moq;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

namespace Spg.AutoTeileShop.ApplicationTest.Mock
{
    public class Product_ServiceTestMock
    {
        private readonly Mock<ProductRepository> _productRepositoryMock = new Mock<ProductRepository>();
        ProductService _productService;

        public Product_ServiceTestMock()
        {
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void Create_Product_Succes_Test_Mock()
        {
            //Arrange
            
            Product product = new Product()
            {
                Ean13 = "dagasgasf",
                Description = "Des Test",
                Guid = Guid.NewGuid(),
                Name = "Pro Test",
                Price = 499.99M,
                Stock = 1               
            };
            _productRepositoryMock
                .Setup(r => r.Add(product))
                .Returns(product);
            //Act

            _productService.Add(product);

            //Assert
            _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        }
    }
}