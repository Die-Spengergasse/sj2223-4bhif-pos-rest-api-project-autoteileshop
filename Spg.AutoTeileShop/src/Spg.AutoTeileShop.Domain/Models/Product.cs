﻿using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
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
        public int Id { get; private set; }
        public Guid Guid { get;  set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public virtual Catagory? catagory { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string Ean13 { get; set; } = string.Empty;
        public QualityType Quality { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }
        public DateTime receive { get; set; }

        // n zu m Relation
        private List<Car> _productFitsForCar = new();
        public virtual IReadOnlyList<Car> ProductFitsForCar => _productFitsForCar;

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

        public Product( Guid guid, string name, decimal price, Catagory? catagory, string description, string? image, QualityType quality, int stock, int discount, DateTime receive, List<Car> productFitsForCar)
        {
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
            _productFitsForCar.AddRange(productFitsForCar);
        }

        public Product()
        {
        }

        public Product(ProductDTO pDTO)
        {
            this.Name = pDTO.Name;
            this.Price = pDTO.Price;
            this.catagory = pDTO.catagory;
            this.Description = pDTO.Description;
            this.Image = pDTO.Image;
            this.Ean13 = pDTO.Ean13;
            this.Quality = pDTO.Quality;
            this.Stock = pDTO.Stock;
            this.Discount = pDTO.Discount;
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

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   Id == product.Id &&
                   Guid.Equals(product.Guid) &&
                   Name == product.Name &&
                   Price == product.Price &&
                   EqualityComparer<Catagory?>.Default.Equals(catagory, product.catagory) &&
                   //Description == product.Description &&
                   Image == product.Image &&
                   Ean13 == product.Ean13 &&
                   Quality == product.Quality &&
                   Stock == product.Stock &&
                   Discount == product.Discount &&
                   receive == product.receive;
                   //EqualityComparer<List<Car>>.Default.Equals(_productFitsForCar, product._productFitsForCar) &&
                   //EqualityComparer<IReadOnlyList<Car>>.Default.Equals(ProductFitsForCar, product.ProductFitsForCar);
        }
    }
}
