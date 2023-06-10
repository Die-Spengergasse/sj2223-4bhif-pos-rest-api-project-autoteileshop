using Spg.AutoTeileShop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required()]
        [MaxLength(20, ErrorMessage = "Name darf nicht länger als 20 Zeichen sein")]
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
            this.Id = product.Id;
            this.Guid = product.Guid;
            this.Name = product.Name;
            this.Price = product.Price;
            this.catagory = product.catagory;
            this.Description = product.Description;
            this.Image = product.Image;
            this.Ean13 = product.Ean13;
            this.Quality = product.Quality;
            this.Stock = product.Stock;
            this.Discount = product.Discount;
        }
    }

}
