using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class UserRegistServic : IUserRegistrationService
    {
        UserRepository _userReopop;
        public UserRegistServic(UserRepository userRepo)
        {
            _userReopop = userRepo;
        }

        public User regist(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW)
        {
            User user = createUser(Vorname, Nachname, Addrese, Telefon, Email, PW);
            _userReopop.SetUser(user);

            SendMail sm = new();
            string code = sm.Send(Email, "", "", "", "");            
            return user;
            
        }
        public User createUser(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW)
        {
            User user = new User();
            user.Vorname = Vorname;
            user.Nachname = Nachname;
            user.Addrese = Addrese;
            user.Telefon = Telefon;
            user.Email = Email;
            user.PW = PW;
            user.Role = Roles.User;
            return user;
        }
    }
}
