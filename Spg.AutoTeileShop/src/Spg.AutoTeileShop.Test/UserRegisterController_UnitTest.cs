using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Spg.AutoTeileShop.API.Controllers;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Test
{
    public class UserRegisterController_UnitTest
    {
        
        private UserRepository _userRepo;
        private UserMailRepo _userMailRepository;
        private UserRegistServic _userRegistServic;
        private RegisterController _registerController;
        private UserMailService _userMailService;

        private RegisterController getController(AutoTeileShopContext db)
        {
            _userRepo = new(db);
            _userMailRepository = new(db);
            _userMailService = new(_userMailRepository);
            _userRegistServic = new(_userRepo, _userMailRepository, _userMailService);
            return _registerController = new(_userRegistServic);
        }

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

        [Fact]
        public void Controller_Register_Test()
        {
            UserRegistDTO userDTOInput = new() { Addrese = "TestAddrese", Email = "davidMailEmpfangTestSPG@web.de", Nachname = "TestNachname", PW = "testPW", Telefon = "133", Vorname = "testVorname" };
            JsonElement userDTOJson = JsonSerializer.SerializeToElement<UserRegistDTO>(userDTOInput);

            var db = createDB();

            var controller = getController(db);
            IActionResult Result = controller.Regist(userDTOJson);

            Assert.IsType<CreatedResult>(Result as CreatedResult);
            Assert.Equal(Result.ToString(), new CreatedResult("/api/User/" + db.Users.FirstOrDefault().Id, db.Users.FirstOrDefault()).ToString());


        }
    }
}
