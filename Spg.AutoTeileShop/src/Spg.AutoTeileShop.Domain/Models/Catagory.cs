﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public enum CategoryTypes { MotorTeile, Bremssystem, Tuning, Optik, Fahrwerk, Elektrik, Verkleidung, Sonstiges }
    
    public class Catagory
    {
        [Key]
        public int Id { get; set; }
        //public Guid Guid { get; set; }
        public Catagory? TopCatagory{ get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryTypes CategoryType { get; set; }

        public Catagory(int id, Catagory? topCatagory, string name, string description, CategoryTypes categoryType)
        {
            Id = id;
            TopCatagory = topCatagory;
            Name = name;
            Description = description;
            CategoryType = categoryType;
        }

        public Catagory()
        {
        }
    }
}
