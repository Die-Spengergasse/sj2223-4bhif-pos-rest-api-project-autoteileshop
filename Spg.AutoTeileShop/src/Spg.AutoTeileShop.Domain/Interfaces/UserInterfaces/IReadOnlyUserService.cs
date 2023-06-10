using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IReadOnlyUserService
    {
        User? GetById(int Id);
        IEnumerable<User> GetAll();
        User? GetByGuid(Guid guid);
    }
}
