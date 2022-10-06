﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class Car
    {
        public long Id { get; set; }
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }
        private List<Product> _fitsForProducts { get; set; } = new();
        public IReadOnlyList<Product> FitsForProducts => _fitsForProducts;

        public void AddFitsForProducts(Product entity)
        {
            if (entity is not null)
            {
                _fitsForProducts.Add(entity);
                if (!entity.ProductFitsForCar.Contains(this))
                    entity.AddProductFitsForCar(this);
            }
        }

        public void RemoveFitsForProducts(Product entity)
        {
            if (entity is not null)
            {
                _fitsForProducts.Remove(entity);
                if (entity.ProductFitsForCar.Contains(this))
                    entity.RemoveProductFitsForCar(this);
            }
        }
    }
}