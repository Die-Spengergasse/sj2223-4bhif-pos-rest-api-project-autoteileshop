using Moq;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;
using Spg.AutoTeileShop.ApplicationTest.Helpers;

namespace Spg.AutoTeileShop.ApplicationTest.Mock
{
    public class Product_ServiceTestMock
    {
        private readonly Mock<IProductRepositroy> _productRepositoryMock = new Mock<IProductRepositroy>();
        ProductService _productService;

        public Product_ServiceTestMock()
        {
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void Create_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.Add(product))
                .Returns(MockUtilities.GetSeedingProduct());

            //Act

            _productService.Add(product);

            //Assert
            _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        }
        
        [Fact]
        public void Update_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.Update(product))
                .Returns(MockUtilities.GetSeedingProduct());
            _productRepositoryMock
                .Setup(r => r.GetById(1))
                .Returns(MockUtilities.GetSeedingProduct());
            //Act

            _productService.Update(product);

            //Assert
            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Delete_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.Delete(product))
                .Returns(MockUtilities.GetSeedingProduct());

            //Act

            _productService.Delete(product);

            //Assert
            _productRepositoryMock.Verify(r => r.Delete(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void GetById_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.GetById(1))
                .Returns(MockUtilities.GetSeedingProduct());

            //Act

            _productService.GetById(1);

            //Assert
            _productRepositoryMock.Verify(r => r.GetById(1), Times.Once);
        }

        [Fact]
        public void GetByName_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.GetByName("test"))
                .Returns(MockUtilities.GetSeedingProduct());

            //Act

            _productService.GetByName("test");

            //Assert
            _productRepositoryMock.Verify(r => r.GetByName("test"), Times.Once);
        }

        [Fact]
        public void GetByCatagory_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.GetByCatagory(MockUtilities.GetSeedingCatagory_withoutTopCat()))
                .Returns((IEnumerable<Product>)MockUtilities.GetSeedingProductsList());

            //Act

            _productService.GetByCatagory(MockUtilities.GetSeedingCatagory_withoutTopCat());

            //Assert
            _productRepositoryMock.Verify(r => r.GetByCatagory(It.IsAny<Catagory>()), Times.Once);
        }

        [Fact]
        public void GetAll_Product_Succes_Test_Mock()
        {
            //Arrange

            Product product = MockUtilities.GetSeedingProduct();

            _productRepositoryMock
                .Setup(r => r.GetAll())
                .Returns((IEnumerable<Product>)MockUtilities.GetSeedingProductsList());

            //Act

            _productService.GetAll();

            //Assert
            _productRepositoryMock.Verify(r => r.GetAll(), Times.Once);
        }
    }
}