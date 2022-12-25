﻿using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IUserRegistrationService
    {
        public List<Object> regist(string Vorname, string Nachname, string Addrese, string Telefon, string Email, string PW, string FromMail);
        public bool CheckCode(string Mail, string code);
    }
}
