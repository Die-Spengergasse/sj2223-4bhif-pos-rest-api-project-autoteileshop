using Spg.AutoTeileShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Authentication
{
    public interface IDbAuthService
    {
        (UserUpdateDTO?, bool) CheckCredentials(string username, ReadOnlySpan<char> password);

    }
}
