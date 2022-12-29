using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class UserMailConfirme
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public User User { get; set; }
        public string Code { get; set; }

        public UserMailConfirme(int id, User user, string code)
        {
            Id = id;
           // UserId = userId;
            User = user;
            Code = code;
        }

        public UserMailConfirme( User user, string code)
        {
          //  UserId = userId;
            User = user;
            Code = code;
        }

        public UserMailConfirme()
        {
        }
    }
    
}
