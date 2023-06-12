using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class UserGetDTO
    {
        public UserGetDTO(User user)
        {
            Id = user.Id;
            Guid = user.Guid;
            Vorname = user.Vorname;
            Nachname = user.Nachname;
            Addrese = user.Adresse;
            Telefon = user.Telefon;
            Email = user.Email;
            Role = user.Role;
            Confirmed = user.Confirmed;

        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;         //IsUnique
        public Roles Role { get; set; }
        public bool Confirmed { get; set; }


    }
}
