using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Vorname { get; set; } = String.Empty;
        public string Nachname { get; set; } = String.Empty;
        public string Strasse { get; set; } = String.Empty;
        public string Telefon { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        
    }
}
