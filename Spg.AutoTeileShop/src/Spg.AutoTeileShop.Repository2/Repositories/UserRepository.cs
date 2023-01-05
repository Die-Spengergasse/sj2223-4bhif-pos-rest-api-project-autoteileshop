using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public User? Delete(User user)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAll()
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


        public string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public User? GetByGuid(Guid guid)
        {
            return _db.Users.Where(u => u.Guid == guid).SingleOrDefault();
        }
    }
}
