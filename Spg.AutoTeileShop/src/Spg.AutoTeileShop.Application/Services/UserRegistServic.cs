using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class UserRegistServic : IUserRegistrationService
    {
        UserRepository _userReopop;
        UserMailRepo _userMailRepository;
        public UserRegistServic(UserRepository userRepo, UserMailRepo userMailRepository)
        {
            _userReopop = userRepo;
            _userMailRepository = userMailRepository;
        }

        public User regist(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW)
        {
            User user = createUser(Vorname, Nachname, Addrese, Telefon, Email, PW);
            _userReopop.SetUser(user);
            
            SendMail sm = new();
            string code = sm.Send(Email, "", "", "", "");

            UserMailService _userMailService = new(_userMailRepository);
            UserMailConfirme userMailConfirmes = new(user.Id, user, sha256_hash(code));
            _userMailService.SetUserMailConfirme(userMailConfirmes);

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

        public static String sha256_hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }

        public bool CheckCode(string Mail,string code)
        {
         
            
        }
    }
}
