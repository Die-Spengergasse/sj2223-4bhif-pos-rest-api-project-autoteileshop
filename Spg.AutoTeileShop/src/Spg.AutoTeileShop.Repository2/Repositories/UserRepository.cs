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
        private AutoTeileShopContext _db;

        public UserRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public User? Delet(User user)
        {
            _db.Users.Remove(user);
            return user;
        }

        public IReadOnlyList<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User? GetByEMail(string email)
        {
            return _db.Users.Where(u => u.Email == email).SingleOrDefault();
        }

        public User? GetByEMailAndPassword(string email, string password)
        {
            return _db.Users.Where(u => u.Email == email && u.PW == password).SingleOrDefault();
        }

        public User? GetById(int Id)
        {
            return _db.Users.Find(Id);
        }

        public User? GetByName(string name)
        {
            return _db.Users.Where(u => u.Nachname == name).SingleOrDefault();
        }

        public User? SetUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User? UpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
            return user;
        }
    }
}
