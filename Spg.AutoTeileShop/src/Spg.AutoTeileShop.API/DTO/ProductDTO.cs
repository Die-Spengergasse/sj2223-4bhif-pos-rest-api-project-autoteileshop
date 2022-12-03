using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.DTO
{
    public class ProductDTO
    {
        public int Id { get; private set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Catagory? catagory { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string Ean13 { get; set; } = string.Empty;
        public QualityType Quality { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }

        public ProductDTO(Product product)
        {
            Id = product.Id;
            Guid = product.Guid;
            Name = product.Name;
            Price = product.Price;
            this.catagory = product.catagory;
            Description = product.Description;
            Image = product.Image;
            Ean13 = product.Ean13;
            Quality = product.Quality;
            Stock = product.Stock;
            Discount = product.Discount;
        }
    }
    
}
