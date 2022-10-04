using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class ProductFitsForCar
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public Car? CarNav { get; set; }
        public long ProductId { get; set; }
        public Product? ProductNav { get; set; }
    }
}
