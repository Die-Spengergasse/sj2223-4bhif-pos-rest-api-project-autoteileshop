using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class UserMailService : IUserMailService
    {
        private readonly IUserMailRepo _userMailRepo;
        public UserMailService(IUserMailRepo userMailRepo)
        {
            _userMailRepo = userMailRepo;
        }
        public UserMailConfirme? GetUserMailConfirmeById(int Id)
        {
            return _userMailRepo.GetById(Id);
        }
        public UserMailConfirme? GetUserMailConfirmeByMail(string mail)
        {
            return _userMailRepo.GetByMail(mail);
        }
        public UserMailConfirme? SetUserMailConfirme(UserMailConfirme userMailConfirme)
        {
            return _userMailRepo.SetUserMailConfirme(userMailConfirme);
        }
    }
}
