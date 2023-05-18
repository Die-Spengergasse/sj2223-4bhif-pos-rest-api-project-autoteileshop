using Spg.AutoTeileShop.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Helper
{
    public class HateoasBuild<T> where T : class
    {
        public string Methode { get; set; }

        public string RoutenPatternString { get; set; }

        public string Href { get; set; }

        public List<HateoasObject<T>> buildHateoas(List<T> values, List<BuildRoutePattern> routes)
        {
            return null;
        }
       
    }
}
