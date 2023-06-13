using Spg.AutoTeileShop.Domain.DTO;

namespace Spg.AutoTeileShop.Domain.Interfaces.Authentication
{
    public interface IDbAuthService
    {
        (UserUpdateDTO?, bool) CheckCredentials(string username, ReadOnlySpan<char> password);

    }
}
