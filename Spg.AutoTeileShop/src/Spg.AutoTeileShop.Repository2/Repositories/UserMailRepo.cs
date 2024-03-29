﻿using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class UserMailRepo : IUserMailRepo
    {
        private readonly AutoTeileShopContext _db;

        public UserMailRepo(AutoTeileShopContext db)
        {
            _db = db;
        }

        public bool DeletAllUserMailbyMail(string mail)
        {
            _db.UserMailConfirms.RemoveRange(_db.UserMailConfirms.Where(x => x.User.Email == mail));
            _db.SaveChanges();
            return true;
        }

        public bool DeletUserMailbyId(int Id)
        {
            _db.UserMailConfirms.Remove(_db.UserMailConfirms.Find(Id));
            return true;
        }

        public UserMailConfirme? GetById(int Id)
        {
            return _db.UserMailConfirms.Where(u => u.Id == Id).SingleOrDefault();// ?? throw Exception.("User mit der Id: " + Id + " wurd nicht gefunde");
        }

        public UserMailConfirme? GetByMail(string mail)
        {
            return _db.UserMailConfirms.Where(u => u.User.Email == mail).SingleOrDefault();
        }

        public UserMailConfirme? SetUserMailConfirme(UserMailConfirme userMailConfirme)
        {
            _db.UserMailConfirms.Add(userMailConfirme);
            _db.SaveChanges();
            return userMailConfirme;
        }

        public string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
