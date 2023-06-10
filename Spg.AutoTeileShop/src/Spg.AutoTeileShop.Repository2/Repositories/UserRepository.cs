using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Security.Cryptography;
using System.Text;

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

        public User? GetByGuid(Guid guid)
        {
            return _db.Users.Where(u => u.Guid == guid).SingleOrDefault();
        }

        public string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }


        public string GenerateSalt()
        {
            // 128bit Salt erzeugen.
            byte[] salt = new byte[128 / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public string CalculateHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash = new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            // Das Bytearray wird Base64 codiert zurückgegeben.
            string hashedPassword = Convert.ToBase64String(hashedData);
            Console.WriteLine($"Salt:            {salt}");
            Console.WriteLine($"Password:        {password}");
            Console.WriteLine($"Hashed Password: {hashedPassword}");
            return hashedPassword;
        }

        bool CheckPassword(string password, string salt, string hashedPassword) =>
            hashedPassword == CalculateHash(password, salt);
    }
}
