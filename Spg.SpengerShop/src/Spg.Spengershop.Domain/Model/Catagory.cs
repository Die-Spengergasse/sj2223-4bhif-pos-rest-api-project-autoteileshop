using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Spengershop.Domain.Model
{
    public enum CategoryTypes { Food, Drink, Clothes, Electronics, Other }
    public class Catagory
    {
        public long Id { get; set; }
        string Name { get; set; } = string.Empty;
        public CategoryTypes CategoryTypes { get; set; }
    }
}
