using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class EntityBase
    {
        public int Id { get; private set; } // PK
        public DateTime? LastChangeDate { get; set; }
    }
}
