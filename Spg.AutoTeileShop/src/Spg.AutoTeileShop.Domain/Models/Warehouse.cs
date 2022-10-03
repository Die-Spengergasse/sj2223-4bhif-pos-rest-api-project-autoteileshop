using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public enum QualityType { SehrGut, Gut, Mittel, Schlecht, SehrSchlecht }

    public class Warehouse
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long  ProdcutId { get; set; }
        public Product? Product { get; set; }
        public QualityType Quality { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public DateTime receive { get; set; }        
    }
}
