using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int Id);
        User? GetByGuid(Guid guid);

        User? GetByName(string name);
        User? GetByEMail(string email);
        User? GetByEMailAndPassword(string email, string password);
        User? SetUser(User user);
        User? UpdateUser(User user);
        User? Delete(User user);
        string ComputeSha256Hash(string value);
        string GenerateSalt();
        string CalculateHash(string password, string salt);



    }
}
