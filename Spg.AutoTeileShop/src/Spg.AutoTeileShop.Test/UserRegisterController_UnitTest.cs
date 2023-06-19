using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.API.Controllers.V1;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

namespace Spg.AutoTeileShop.Domain.Test
{
    [Collection("Sequential tests")]
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

        public static string ReadLineWithQuestionMark()
        {
            string relativeFilePath = "DataSource.txt";
            string currentDirectory = Environment.CurrentDirectory;
            int endIndex;
            string extractedPath;
            string filePath;
            if (currentDirectory.Contains($"\\src\\"))
            {
                endIndex = currentDirectory.IndexOf($"\\src\\") + $"\\src\\".Length;
                extractedPath = currentDirectory.Substring(0, endIndex);
                filePath = Path.Combine(extractedPath, relativeFilePath);
            }
            else
            {
                endIndex = currentDirectory.IndexOf($"\\tests\\");
                extractedPath = currentDirectory.Substring(0, endIndex);
                //filePath = Path.Combine(extractedPath, $"\\src\\", relativeFilePath);
                filePath = extractedPath + $"\\src\\" + relativeFilePath;
            }


            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Die Datei wurde nicht gefunden.", relativeFilePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.TrimStart().StartsWith("?"))
                {
                    return line.TrimStart('?').Trim();
                }
            }

            return null;
        }

        private AutoTeileShopContext createDB()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                  //.UseSqlite(ReadLineWithQuestionMark())
                  .UseSqlite("Data Source = :memory:")     //Home PC       
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

            var db = createDB();

            RegisterController controller = getController(db);
            //IActionResult Result = controller.Register(userDTOInput);

            //Assert.IsType<CreatedResult>(Result as CreatedResult);
            //Assert.Equal(Result.ToString(), new CreatedResult("/api/User/" + db.Users.FirstOrDefault().Id, db.Users.FirstOrDefault()).ToString());


        }

    }
}
