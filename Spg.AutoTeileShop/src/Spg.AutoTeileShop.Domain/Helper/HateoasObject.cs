using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Helper
{
    public class HateoasObject<T> where T : class
    {
        public T objekt { get; set; }
        public List<string> urls = new List<string>();

        public HateoasObject(T _objekt, List<string> urls)
        {
            objekt = _objekt;
            this.urls = urls;
        }
    }
}
