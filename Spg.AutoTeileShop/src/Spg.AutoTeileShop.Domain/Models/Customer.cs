using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Strasse { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PW { get; set; } = string.Empty;

        public Customer()
        {
        }

        public Customer(int id, Guid guid, string vorname, string nachname, string strasse, string telefon, string email, string pW)
        {
            Id = id;
            Guid = guid;
            Vorname = vorname;
            Nachname = nachname;
            Strasse = strasse;
            Telefon = telefon;
            Email = email;
            PW = pW;
        }
    }
}
