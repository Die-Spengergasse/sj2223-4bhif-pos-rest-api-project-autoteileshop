using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public enum GualityType { SehrGut, Gut, Mittel, Schlecht, SehrSchlecht }

    public class Stock
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long  ProdcutId { get; set; }
        public Product Product { get; set; }
        public GualityType Guality { get; set; }
        public int Quantity { get; set; }
        public int number { get; set; }
        public int Discount { get; set; }
        public DateTime receive { get; set; }        
    }
}
