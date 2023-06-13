using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Authentication;

namespace Spg.AutoTeileShop.Application.Services
{
    public class DbAuthService : IDbAuthService
    {
        public (UserUpdateDTO?, bool) CheckCredentials(string email, ReadOnlySpan<char> password)
        {
            // PW Hashing
            if (email == "test" || password == "testPW")
            {
                UserUpdateDTO user = new UserUpdateDTO()
                {
                    Vorname = "test",
                    Addrese = "test",
                    Email = "test",
                    Nachname = "test",
                    Telefon = "0676636989",
                    Role = Domain.Models.Roles.User,

                };

                return (user, true);
            }
            return (null, false);
        }

    }
}
