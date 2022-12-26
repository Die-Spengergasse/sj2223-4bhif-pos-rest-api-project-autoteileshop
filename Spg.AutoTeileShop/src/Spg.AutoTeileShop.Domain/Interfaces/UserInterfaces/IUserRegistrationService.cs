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
        public List<Object> Register_sendMail_Create_User(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW, string FromMail);
        public bool CheckCode_and_verify(string Mail, string code);
    }
}
