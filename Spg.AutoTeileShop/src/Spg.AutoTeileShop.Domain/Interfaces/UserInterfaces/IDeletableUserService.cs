using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IDeletableUserService
    {
        User? Delete(User user);
    }
}
