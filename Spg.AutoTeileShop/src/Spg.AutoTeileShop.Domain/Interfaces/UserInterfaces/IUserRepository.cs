using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        public User? GetByName(string name);
        public User? GetByEMail(string email);
        public User? GetByEMailAndPassword(string email, string password);
        public User? SetUser(User user);
        public User? UpdateUser(User user);
        
        
    }
}
