using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class ProductDTOFilter
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? catagoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string Ean13 { get; set; } = string.Empty;
        public QualityType Quality { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }

        public ProductDTOFilter(Product product)
        {
            this.Id = product.Id;
            this.Guid = product.Guid;
            this.Name = product.Name;
            this.Price = product.Price;
            this.catagoryId = product.catagory.Id;
            this.Description = product.Description;
            this.Image = product.Image;
            this.Ean13 = product.Ean13;
            this.Quality = product.Quality;
            this.Stock = product.Stock;
            this.Discount = product.Discount;
        }

        public override string ToString()
        {
            return $"{{\"Id\": {Id}, \"Guid\": \"{Guid}\", \"Name\": \"{Name}\", \"Price\": {Price}, \"catagory\": {catagoryId}, \"Description\": \"{Description}\", \"Image\": \"{Image}\", \"Ean13\": \"{Ean13}\", \"Quality\": \"{Quality}\", \"Stock\": {Stock}, \"Discount\": {Discount}}}";
        }
    }

}
