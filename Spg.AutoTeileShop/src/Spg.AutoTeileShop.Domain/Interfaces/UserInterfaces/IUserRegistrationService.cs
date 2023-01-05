using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IUserRegistrationService
    {
        public IEnumerable<Object> Register_sendMail_Create_User(User postUser, string FromMail);
        public bool CheckCode_and_verify(string Mail, string code);
    }
}
