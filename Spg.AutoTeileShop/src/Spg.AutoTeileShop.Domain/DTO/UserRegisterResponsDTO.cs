using Spg.AutoTeileShop.Domain.Models;
using System.Text;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class UserRegisterResponsDTO
    {

        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;         //IsUnique

        public UserRegisterResponsDTO(User user)
        {
            Vorname = user.Vorname;
            Nachname = user.Nachname;
            Addrese = user.Adresse;
            Telefon = user.Telefon;
            Email = user.Email;
        }

        public UserRegisterResponsDTO()
        {
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.AppendFormat("\"Vorname\": \"{0}\", ", Vorname);
            builder.AppendFormat("\"Nachname\": \"{0}\", ", Nachname);
            builder.AppendFormat("\"Adresse\": \"{0}\", ", Addrese);
            builder.AppendFormat("\"Telefon\": \"{0}\", ", Telefon);
            builder.AppendFormat("\"Email\": \"{0}\", ", Email);
            builder.Append("}");
            return builder.ToString();
        }
    }
}
