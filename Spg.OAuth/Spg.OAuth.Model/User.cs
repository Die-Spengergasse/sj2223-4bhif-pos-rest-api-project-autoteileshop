using System;
using System.ComponentModel.DataAnnotations;

namespace Spg.OAuth.Model
{
    public class User
    {
        public enum Roles
        { User, Admin, Salesman }
        [Key]
        public int Id { get; private set; }
        public Guid Guid { get; set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;         //IsUnique
        public string PW { get; set; } = string.Empty;
        public Roles Role { get; set; }
        public bool Confirmed { get; set; }

        public User()
        {
        }

        public User
        (int id, Guid guid, string vorname, string nachname,
        string addrese, string telefon, string email, string pW, Roles role, bool confirmed)
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
        (Guid guid, string vorname, string nachname,
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

    }
}
