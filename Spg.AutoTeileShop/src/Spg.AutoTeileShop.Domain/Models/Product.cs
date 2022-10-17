using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models

{
    public enum QualityType { SehrGut, Gut, Mittel, Schlecht, SehrSchlecht }

    public class Product
    {
        [Key]
        public int Id { get; set; }
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
        public DateTime receive { get; set; }

        // n zu m Relation
        private List<Car> _productFitsForCar = new();
        public IReadOnlyList<Car> ProductFitsForCar => _productFitsForCar;

        public Product(int id, Guid guid, string name, decimal price, Catagory? catagory, string description, string? image, QualityType quality, int stock, int discount, DateTime receive, List<Car> productFitsForCar)
        {
            Id = id;
            Guid = guid;
            Name = name;
            Price = price;
            this.catagory = catagory;
            Description = description;
            Image = image;
            Quality = quality;
            Stock = stock;
            Discount = discount;
            this.receive = receive;
            _productFitsForCar = productFitsForCar;
        }

        public Product()
        {
        }

        public void AddProductFitsForCar(Car entity)
        {
            if (entity is not null)
            {
                _productFitsForCar.Add(entity);
                if (!entity.FitsForProducts.Contains(this))
                    entity.AddFitsForProducts(this);
            }  
                
        }
        public void RemoveProductFitsForCar(Car entity)
        {
            if (entity is not null)
            {
                _productFitsForCar.Remove(entity);
                if (entity.FitsForProducts.Contains(this))
                    entity.RemoveFitsForProducts(this);
            }
        }
    }
}
