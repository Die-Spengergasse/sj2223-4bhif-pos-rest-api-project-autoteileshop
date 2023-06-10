using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IAddUpdateableUserService
    {
        User? Add(User user);
        User? Update(Guid guid, User user);
    }
}
