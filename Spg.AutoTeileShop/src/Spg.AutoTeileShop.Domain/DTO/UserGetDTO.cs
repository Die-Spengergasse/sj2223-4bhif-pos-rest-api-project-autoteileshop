using Spg.AutoTeileShop.Domain.Models;
using System.Text;

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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.AppendFormat("\"Id\": {0}, ", Id);
            builder.AppendFormat("\"Guid\": \"{0}\", ", Guid);
            builder.AppendFormat("\"Vorname\": \"{0}\", ", Vorname);
            builder.AppendFormat("\"Nachname\": \"{0}\", ", Nachname);
            builder.AppendFormat("\"Adresse\": \"{0}\", ", Addrese);
            builder.AppendFormat("\"Telefon\": \"{0}\", ", Telefon);
            builder.AppendFormat("\"Email\": \"{0}\", ", Email);
            builder.AppendFormat("\"Role\": \"{0}\", ", Role);
            builder.AppendFormat("\"Confirmed\": {0}", Confirmed);
            builder.Append("}");
            return builder.ToString();
        }
    }
}
