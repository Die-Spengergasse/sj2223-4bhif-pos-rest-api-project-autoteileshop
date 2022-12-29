using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
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
        IUserRepository _userRepo;
        IUserMailRepo _userMailRepository;
        IUserMailService _userMailService;
        public UserRegistServic(IUserRepository userRepo, IUserMailRepo userMailRepository, IUserMailService userMailService)
        {
            _userRepo = userRepo;
            _userMailRepository = userMailRepository;
            _userMailService = userMailService;
        }

        public List<Object> Register_sendMail_Create_User(User postUser, string fromMail) //List<Object> 
        {
            if (fromMail.Count() == 0) { fromMail = "mailtestdavid01@gmail.com"; }
            
            postUser.Confirmed = false;
            postUser.Role = Roles.User;
            postUser.Guid = Guid.NewGuid();
            postUser.PW = sha256_hash(postUser.PW);
            User user = _userRepo.SetUser(postUser);
            
            SendMail sm = new();
            string code = sm.Send(user.Email, fromMail, user.Email, "", "");

            //UserMailService _userMailService = new(_userMailRepository);
            _userMailRepository.DeletAllUserMailbyMail(user.Email);
            UserMailConfirme userMailConfirmes = new(user, sha256_hash(code));
            _userMailService.SetUserMailConfirme(userMailConfirmes);


            List<Object> obj = new();
            obj.Add(user);
            obj.Add(code); //for Tests
            
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

        public String sha256_hash(String value)
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
                    _userRepo.UpdateUser(checkUserMailConf.User);
                    _userMailRepository.DeletUserMailbyId(checkUserMailConf.Id);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
