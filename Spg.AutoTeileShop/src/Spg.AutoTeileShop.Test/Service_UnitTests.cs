using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

namespace Spg.AutoTeileShop.Domain.Test
{
    public class Service_UnitTests
    {
        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite("Data Source=AutoTeileShopTest.db")
                  //.UseSqlite(@"Data Source= D:/4 Klasse/Pos1 Repo/sj2223-4bhif-pos-rest-api-project-autoteileshop/Spg.AutoTeileShop/src/AutoTeileShop.db")      //Laptop
                  .UseSqlite("Data Source = I:\\Dokumente 4TB\\HTL\\4 Klasse\\POS1 Git Repo\\sj2223-4bhif-pos-rest-api-project-autoteileshop\\Spg.AutoTeileShop\\src\\AutoTeileShop.db")     //Home PC       
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //db.Seed();
            return db;
        }

        private UserMailService Get_Service_UserMail(AutoTeileShopContext _db) 
        {
            UserMailRepo userMailRepo = new UserMailRepo(_db);
            return new UserMailService(userMailRepo);
        }

        private UserRegistServic Get_Service_UserRegist(AutoTeileShopContext _db) 
        {
            UserRepository userRepo = new(_db);
            UserMailRepo userMailRepo = new(_db);
            return new UserRegistServic(userRepo, userMailRepo);
            
        }

        [Fact]
        public void Service_SendMail_Test()
        {
            AutoTeileShopContext db = createDB();
            UserRegistServic userRegist = Get_Service_UserRegist(db);

            User user =  (User)userRegist.Register_sendMail_Create_User("TestVorname", "TestNachname", "TestAdresse","06762656646" , "davidMailEmpfangTestSPG@web.de", "TestPasswort", "mailtestdavid01@gmail.com")[0];

        }

        [Fact]
        public void Service_SendMail_and_Check_Code_Test()
        {
            AutoTeileShopContext db = createDB();
            UserRegistServic userRegist = Get_Service_UserRegist(db);

            var obj = userRegist.Register_sendMail_Create_User("TestVorname", "TestNachname", "TestAdresse", "06762656646", "davidMailEmpfangTestSPG@web.de", "TestPasswort", "mailtestdavid01@gmail.com");

            UserMailService userMailService = Get_Service_UserMail(db);

            User user = (User)obj.First();
            UserMailConfirme userMailConfirme = userMailService.GetUserMailConfirmeByMail(user.Email);

            Assert.Equal(userMailConfirme.Code, obj.Last());
        }

    }
}
