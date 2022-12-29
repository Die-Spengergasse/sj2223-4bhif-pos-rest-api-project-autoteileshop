using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface
{
    public interface IUserMailRepo
    {
        UserMailConfirme? GetById(int Id);
        UserMailConfirme? GetByMail(string mail);
        UserMailConfirme? SetUserMailConfirme(UserMailConfirme userMailConfirme);
        bool DeletUserMailbyId(int Id);
        bool DeletAllUserMailbyMail(string mail);
    }
}
