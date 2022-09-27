using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Spengershop.Domain.Model
{
    public enum CategoryTypes { Food, Drink, Clothes, Electronics, Other }
    public class Catagory
    {
        string Name { get; set; } = string.Empty;
        public CategoryTypes CategoryTypes { get; set; }
    }
}
