using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface
{
    public interface IUserMailService
    {
        public UserMailConfirme SetUserMailConfirme(UserMailConfirme uMC);
    }
}
