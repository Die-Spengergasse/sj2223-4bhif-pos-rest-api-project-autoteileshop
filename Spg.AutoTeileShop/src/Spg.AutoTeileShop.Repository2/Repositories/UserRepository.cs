using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AutoTeileShopContext _db;

        public UserRepository(AutoTeileShopContext db)
        {
            _db = db;

        }

        public User? GetByEMail(string email)
        {
            throw new NotImplementedException();
        }

        public User? GetByEMailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public User? GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public User? SetUser(User user)
        {
            _db.Add(user);
            _db.SaveChanges();
            return user;
        }
    }
}
