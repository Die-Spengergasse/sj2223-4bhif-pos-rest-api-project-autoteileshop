using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Spengershop.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        public decimal Price { get; set; }
        public int Menge { get; set; }
    }
}
