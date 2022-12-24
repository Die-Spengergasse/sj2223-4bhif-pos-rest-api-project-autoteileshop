using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class UserMailRepo : IUserMailRepo
    {
        private readonly AutoTeileShopContext _db;

        public UserMailRepo(AutoTeileShopContext db)
        {
            _db = db;
        }

        public UserMailConfirme? GetById(int Id)
        {
            return _db.UserMailConfirms.Where(u => u.Id == Id).SingleOrDefault();// ?? throw Exception.("User mit der Id: " + Id + " wurd nicht gefunde");
        }

        public UserMailConfirme? GetByMail(string mail)
        {
            throw new NotImplementedException();
        }

        public UserMailConfirme? SetUserMailConfirme(UserMailConfirme userMailConfirme)
        {
            _db.UserMailConfirms.Add(userMailConfirme);
            _db.SaveChanges();
            return userMailConfirme;
        }
    }
}
