using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Authentication;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class DbAuthService : IDbAuthService
    {
        public (UserUpdateDTO?, bool) CheckCredentials(string email, ReadOnlySpan<char> password)
        {
            // PW Hashing
            if (email == "test" || password == "testPW")
            {
                UserUpdateDTO user = new UserUpdateDTO() { 
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
