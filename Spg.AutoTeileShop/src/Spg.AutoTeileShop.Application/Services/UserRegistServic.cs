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

        public IEnumerable<Object> Register_sendMail_Create_User(User postUser, string fromMail) //List<Object> 
        {
            if (fromMail.Count() == 0) { fromMail = "mailtestdavid01@gmail.com"; }

            postUser.Confirmed = false;
            postUser.Role = Roles.User;
            postUser.Guid = Guid.NewGuid();
            postUser.PW = _userRepo.ComputeSha256Hash(postUser.PW);
            User user = _userRepo.SetUser(postUser);

            SendMail sm = new();
            //if (!sm.ValidateMail(user.Email)) throw new Exception("Mail is not valid");
            string code = sm.Send(user.Email, fromMail, user.Email, "", "");

            _userMailRepository.DeletAllUserMailbyMail(user.Email);
            UserMailConfirme userMailConfirmes = new(user, _userRepo.ComputeSha256Hash(code), DateTime.Now);
            _userMailService.SetUserMailConfirme(userMailConfirmes);


            List<Object> obj = new();
            obj.Add(user);
            obj.Add(code); //for Tests

            return obj;

        }


        public bool CheckCode_and_verify(string Mail, string code)
        {
            UserMailConfirme checkUserMailConf = _userMailRepository.GetByMail(Mail);
            if (checkUserMailConf != null)
            {
                if (checkUserMailConf.date.AddMinutes(15) <= DateTime.Now) 
                {
                    throw new Exception("Code ist abgelaufen");
                }
                if (checkUserMailConf.Code == _userRepo.ComputeSha256Hash(code))
                {
                    checkUserMailConf.User.Confirmed = true;
                    _userRepo.UpdateUser(checkUserMailConf.User);
                    _userMailRepository.DeletUserMailbyId(checkUserMailConf.Id);
                    return true;
                }
                throw new Exception("Falscher Code");
            }
            throw new Exception("Es wurde keine passende Mail gefunden");
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

        
    }
}
