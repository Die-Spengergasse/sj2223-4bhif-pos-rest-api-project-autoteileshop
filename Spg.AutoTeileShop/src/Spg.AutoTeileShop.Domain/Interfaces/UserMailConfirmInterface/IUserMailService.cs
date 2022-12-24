using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface
{
    public interface IUserMailService
    {
        public UserMailConfirme SetUserMailConfirme(UserMailConfirme uMC);
    }
}
