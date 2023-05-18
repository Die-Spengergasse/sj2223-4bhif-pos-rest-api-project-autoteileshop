using Microsoft.AspNetCore.Routing.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Helper
{
    public class BuildRoutePattern
    {
        public string RoutenPatternString { get; set; }

        public string Methode { get; set; }

        public BuildRoutePattern(string routenPatternString, string methode)
        {
            Methode = methode;
            RoutenPatternString = routenPatternString;
        }

        public override bool Equals(object? obj)
        {
            return obj is BuildRoutePattern pattern &&
                   RoutenPatternString == pattern.RoutenPatternString &&
                   Methode == pattern.Methode;
        }
    }
}
