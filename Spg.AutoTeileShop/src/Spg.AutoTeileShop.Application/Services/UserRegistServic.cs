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

        public List<Object> Register_sendMail_Create_User(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW, string FromMail) //List<Object> 
        {
            User user = _userReopop.SetUser(createUser(Vorname, Nachname, Addrese, Telefon, Email, PW));
            
            SendMail sm = new();
            string code = sm.Send(Email, FromMail, Email, "", "");

            UserMailService _userMailService = new(_userMailRepository);
            UserMailConfirme userMailConfirmes = new(user.Id, user, sha256_hash(code));
            _userMailService.SetUserMailConfirme(userMailConfirmes);


            List<Object> obj = new();
            obj.Add(user);
            obj.Add(sha256_hash(code)); //for Tests

            return obj;
            
        }
        private User createUser(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW)
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

        private static String sha256_hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
        
        
        public bool CheckCode_and_verify(string Mail,string code)
        {
            UserMailConfirme checkUserMailConf = _userMailRepository.GetByMail(Mail);
            if (checkUserMailConf != null)
            {
                if (checkUserMailConf.Code == sha256_hash(code))
                {
                    checkUserMailConf.User.Confirmed = true;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
