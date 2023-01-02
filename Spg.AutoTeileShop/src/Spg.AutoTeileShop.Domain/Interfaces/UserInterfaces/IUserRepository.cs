﻿using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        IReadOnlyList<User> GetAll();
        User? GetById (int Id);
        User? GetByGuid(Guid guid);

        User? GetByName(string name);
        User? GetByEMail(string email);
        User? GetByEMailAndPassword(string email, string password);
        User? SetUser(User user);
        User? UpdateUser(User user);
        User? Delete(User user);
        String sha256_hash(String value);


    }
}
