using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface
{
    public interface IUserMailRepo
    {
        UserMailConfirme? GetById(int Id);
        UserMailConfirme? GetByMail(string mail);
        UserMailConfirme? SetUserMailConfirme(UserMailConfirme userMailConfirme);
        bool DeletUserMailbyId(int Id);
        bool DeletAllUserMailbyMail(string mail);
        string ComputeSha256Hash(string value);
    }
}
