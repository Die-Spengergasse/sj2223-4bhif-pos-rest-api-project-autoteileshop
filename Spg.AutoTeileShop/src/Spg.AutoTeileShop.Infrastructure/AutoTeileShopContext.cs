using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Models;
using System.Security.Cryptography;
using System.Text;
using Randomizer = Bogus.Randomizer;

namespace Spg.AutoTeileShop.Infrastructure
{
    public class AutoTeileShopContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Catagory> Catagories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<UserMailConfirme> UserMailConfirms { get; set; }


        public AutoTeileShopContext(DbContextOptions options) : base(options)
        {
        }

        protected AutoTeileShopContext() : this(new DbContextOptions<DbContext>())
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseLazyLoadingProxies();
            //options.UseSqlite(ReadLineWithQuestionMark());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Car>()
                .HasMany(c => c.FitsForProducts)
                .WithMany(p => p.ProductFitsForCar)
                .UsingEntity<Dictionary<string, object>>(
                "CarProduct",
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
                j => j.HasOne<Car>().WithMany().HasForeignKey("CarId").HasPrincipalKey(nameof(Car.Id)),
                j =>
                {
                    j.HasKey("CarId", "ProductId");
                    j.ToTable("CarProduct"); // Optional: Benenne die Zwischentabelle explizit
                });
          
            modelBuilder.Entity<Catagory>()
                .HasOne(c => c.TopCatagory)
                .WithMany()
                .HasForeignKey(c => c.TopCatagoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.catagory)
                .WithMany()
                .HasForeignKey(p => p.CatagoryId);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(s => s.ShoppingCartItems)
                .WithOne(sci => sci.ShoppingCartNav)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.UserNav)
                .WithMany()
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ProductNav)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCartNav)
                .WithMany(s => s.ShoppingCartItems)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<UserMailConfirme>()
                .HasOne(umc => umc.User)
                .WithMany()
                .HasForeignKey(umc => umc.UserId);


        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1017);

            List<User> users = new Faker<User>("de")
                .Rules((f, c) =>
                {
                    c.Guid = f.Random.Guid();
                    c.Vorname = f.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
                    c.Nachname = f.Name.LastName();
                    c.Email = f.Internet.Email(c.Vorname, c.Nachname);
                    c.Telefon = f.Person.Phone;
                    c.Adresse = f.Address.FullAddress();
                    c.Role = f.Random.Enum<Roles>();
                    c.Salt = GenerateSalt();
                    c.PW = CalculateHash(f.Internet.Password(), c.Salt);

                })
            .Generate(50)
            .ToList();
            Users.AddRange(users);
            User admin = new User()
            {
                Guid = Guid.NewGuid(),
                Vorname = "Admin",
                Nachname = "Admin",
                Email = "admin",
                Salt = GenerateSalt(),
                PW = CalculateHash("admin", GenerateSalt()),
                Role = Roles.Admin
            };
            Users.Add(admin);
            SaveChanges();


            List<Car> cars = new Faker<Car>("de")
            .Rules((f, ca) =>
            {
                ca.Marke = f.Vehicle.Manufacturer();
                ca.Modell = f.Vehicle.Model();
                ca.Baujahr = f.Date.Past(15, DateTime.Now);
            })
            .Generate(60)
            .ToList();

            Cars.AddRange(cars);
            SaveChanges();

            Random random = new Random();

            List<Catagory> catagory = new Faker<Catagory>("de")
            .Rules((f, ca) =>
            {

                ca.Description = f.Name.JobDescriptor();
                Array values = Enum.GetValues(typeof(CategoryTypes));
                ca.CategoryType = (CategoryTypes)values.GetValue(random.Next(values.Length));
                ca.Name = ca.CategoryType.ToString();

            })
            .Generate(20)
            .ToList();



            foreach (Catagory c in catagory)
            {
                if (c.CategoryType == CategoryTypes.MotorTeile || c.CategoryType == CategoryTypes.Bremssystem || c.CategoryType == CategoryTypes.Tuning || c.CategoryType == CategoryTypes.Optik || c.CategoryType == CategoryTypes.Fahrwerk || c.CategoryType == CategoryTypes.Antrieb || c.CategoryType == CategoryTypes.Sonstiges)
                {
                    c.TopCatagory = null;
                }
                else
                {
                    while (c.TopCatagory == null)
                    {
                        Catagory topCatagory = catagory[random.Next(catagory.Count)];
                        if (topCatagory.CategoryType == CategoryTypes.MotorTeile || topCatagory.CategoryType == CategoryTypes.Bremssystem || topCatagory.CategoryType == CategoryTypes.Tuning || topCatagory.CategoryType == CategoryTypes.Optik || topCatagory.CategoryType == CategoryTypes.Fahrwerk || topCatagory.CategoryType == CategoryTypes.Antrieb || topCatagory.CategoryType == CategoryTypes.Sonstiges)
                        {
                            c.TopCatagory = topCatagory;
                        }
                    }
                }
            }

            Catagories.AddRange(catagory);
            SaveChanges();


            List<Product> product = new Faker<Product>("de")
            .Rules((f, p) =>
            {

                p.Guid = f.Random.Guid();
                p.Name = f.Commerce.ProductName();
                p.Price = f.Random.Decimal(0, 1000);
                p.catagory = f.PickRandom(catagory);
                p.Description = f.Commerce.ProductDescription();
                p.Image = f.Image.PicsumUrl();

                Array values = Enum.GetValues(typeof(QualityType));
                Random random = new Random();
                p.Quality = (QualityType)values.GetValue(random.Next(values.Length));

                p.Stock = f.Random.Int(11, 1000);
                p.Discount = f.Random.Int(0, 100);
                p.receive = f.Date.Past(10, DateTime.Now);
                p.AddProductFitsForCar(f.PickRandom(cars));


            })
            .Generate(500)
            .ToList();
            product.AddRange(product);
            SaveChanges();

            List<ShoppingCartItem> shoppingCartItems = new Faker<ShoppingCartItem>("de")
                   .Rules((f, shI) =>
                   {
                       shI.guid = f.Random.Guid();
                       shI.Pieces = f.Random.Int(1, 9);
                       shI.ProductNav = f.PickRandom(product);
                   })
                   .Generate(100)
                   .ToList();
            ShoppingCartItems.AddRange(shoppingCartItems);
            SaveChanges();

            List<ShoppingCartItem> items2 = new();
            items2.AddRange(shoppingCartItems);

            List<ShoppingCart> shoppingCart = new Faker<ShoppingCart>("de")
           .Rules((f, sh) =>
           {
               sh.guid = f.Random.Guid();
               sh.UserNav = f.PickRandom(users);
               for (int i = 0; i < 2; i++)
               {
                   var item = f.PickRandom(items2);
                   sh.AddShoppingCartItem(item);
                   items2.Remove(item);
               }
           })
           .Generate(50)
           .ToList();
            ShoppingCarts.AddRange(shoppingCart);
            SaveChanges();


            List<UserMailConfirme> userMailConfirmes = new Faker<UserMailConfirme>("de")
           .Rules((f, u) =>
           {
               u.User = f.PickRandom(users);
               u.Code = ComputeSha256Hash(Guid.NewGuid().ToString().Substring(0, 8));
               //u.UserId = u.User.Id;
           })
           .Generate(50)
           .ToList();
            UserMailConfirms.AddRange(userMailConfirmes);
            SaveChanges();

        }


        public string ComputeSha256Hash(string value) // from ChatGPT supported
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public string GenerateSalt()
        {
            // 128bit Salt erzeugen.
            byte[] salt = new byte[128 / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
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
        public string CalculateHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash = new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            // Das Bytearray wird Base64 codiert zurückgegeben.
            string hashedPassword = Convert.ToBase64String(hashedData);
            Console.WriteLine($"Salt:            {salt}");
            Console.WriteLine($"Password:        {password}");
            Console.WriteLine($"Hashed Password: {hashedPassword}");
            return hashedPassword;
        }
    }
}
