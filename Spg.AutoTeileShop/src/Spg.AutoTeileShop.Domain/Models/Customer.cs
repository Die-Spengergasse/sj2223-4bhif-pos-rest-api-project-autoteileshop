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
        public int Id { get; private set; }
        public Guid Guid { get;  set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PW { get; set; } = string.Empty;

        public Customer()
        {
        }

        public Customer(int id, Guid guid, string vorname, string nachname, string addrese, string telefon, string email, string pW)
        {
            Id = id;
            Guid = guid;
            Vorname = vorname;
            Nachname = nachname;
            Addrese = addrese;
            Telefon = telefon;
            Email = email;
            PW = pW;
        }
    }
}
