using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spg.AutoTeileShop.Domain.Models
{
    public enum Roles
    { User, Admin, Salesman }
    public class User : IFindableByGuid
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;         //IsUnique
        public string PW { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public Roles Role { get; set; }
        public bool Confirmed { get; set; }

        public User()
        {
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.AppendFormat("\"Id\": {0}, ", Id);
            builder.AppendFormat("\"Guid\": \"{0}\", ", Guid);
            builder.AppendFormat("\"Vorname\": \"{0}\", ", Vorname);
            builder.AppendFormat("\"Nachname\": \"{0}\", ", Nachname);
            builder.AppendFormat("\"Adresse\": \"{0}\", ", Adresse);
            builder.AppendFormat("\"Telefon\": \"{0}\", ", Telefon);
            builder.AppendFormat("\"Email\": \"{0}\", ", Email);
            builder.AppendFormat("\"PW\": \"{0}\", ", PW);
            builder.AppendFormat("\"Salt\": \"{0}\", ", Salt);
            builder.AppendFormat("\"Role\": \"{0}\", ", Role);
            builder.AppendFormat("\"Confirmed\": {0}", Confirmed);
            builder.Append("}");
            return builder.ToString();
        }




        public string ToStringWithPWandSlat()
        {
            return $"Id: {Id}, Guid: {Guid}, Vorname: {Vorname}, Nachname: {Nachname}, Adresse: {Adresse}, Telefon: {Telefon}, Email: {Email}, PW: {PW}, Salt: {Salt}, Role: {Role}, Confirmed: {Confirmed}";
        }


        public User
        (int id, Guid guid, string vorname, string nachname,
        string addrese, string telefon, string email, string pW, Roles role, bool confirmed)
        {
            Id = id;
            Guid = guid;
            Vorname = vorname;
            Nachname = nachname;
            Adresse = addrese;
            Telefon = telefon;
            Email = email;
            PW = pW;
            Role = role;
            Confirmed = confirmed;
        }


        public User
        (Guid guid, string vorname, string nachname,
        string addrese, string telefon, string email, string pW, Roles role, bool confirmed)
        {
            Guid = guid;
            Vorname = vorname;
            Nachname = nachname;
            Adresse = addrese;
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
            Adresse = urDTO.Addrese;
            Telefon = urDTO.Telefon;
            Email = urDTO.Email;
            PW = urDTO.PW;
        }

        public User(UserUpdateDTO uuDTO)
        {
            Vorname = uuDTO.Vorname;
            Nachname = uuDTO.Nachname;
            Adresse = uuDTO.Addrese;
            Telefon = uuDTO.Telefon;
            Email = uuDTO.Email;
            PW = uuDTO.PW;
            Role = uuDTO.Role;
            Confirmed = uuDTO.Confirmed;
        }
    }
}
