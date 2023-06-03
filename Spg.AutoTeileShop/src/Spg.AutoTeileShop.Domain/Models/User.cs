using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public enum Roles 
    { User, Admin, Salesman }
    public class User : IFindableByGuid
    {
        [Key]
        public int Id { get; private set; }
        public Guid Guid { get; set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;         //IsUnique
        public string PW { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public Roles Role { get; set; }
        public bool Confirmed { get; set; }

        public User()
        {
        }

        public User
        (int id, Guid guid, string vorname, string nachname,
        string addrese,string telefon, string email, string pW, Roles role, bool confirmed)
        {
            Id = id;
            Guid = guid;
            Vorname = vorname;
            Nachname = nachname;
            Addrese = addrese;
            Telefon = telefon;
            Email = email;
            PW = pW;
            Role = role;
            Confirmed = confirmed;
        }


        public User
        ( Guid guid, string vorname, string nachname,
        string addrese, string telefon, string email, string pW, Roles role, bool confirmed)
        {
            Guid = guid;
            Vorname = vorname;
            Nachname = nachname;
            Addrese = addrese;
            Telefon = telefon;
            Email = email;
            PW = pW;
            Role = role;
            Confirmed = confirmed;
        }

        public User(UserRegistDTO urDTO)
        {
            Vorname = urDTO.Vorname;
            Nachname = urDTO.Nachname;
            Addrese = urDTO.Addrese;
            Telefon = urDTO.Telefon;
            Email = urDTO.Email;
            PW = urDTO.PW;
        }

        public User (UserUpdateDTO uuDTO)
        {
            Vorname = uuDTO.Vorname;
            Nachname = uuDTO.Nachname;
            Addrese = uuDTO.Addrese;
            Telefon = uuDTO.Telefon;
            Email = uuDTO.Email;
            PW = uuDTO.PW;
            Role = uuDTO.Role;
            Confirmed = uuDTO.Confirmed;
        }
    }
}
