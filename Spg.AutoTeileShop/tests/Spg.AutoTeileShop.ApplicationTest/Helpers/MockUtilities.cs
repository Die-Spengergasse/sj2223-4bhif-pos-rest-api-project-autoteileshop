using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.ApplicationTest.Helpers
{
    public class MockUtilities
    {
        public static Car GetSeedingCar()
        {
            return new Car()
            {
                Baujahr = DateTime.Today,
                Marke = "BMW",
                Modell = "M3"
            };
        }

        public static Product GetSeedingProduct()
        {
            return new Product(1, Guid.NewGuid(), "test", 50.0m, GetSeedingCatagory_withoutTopCat(), "test",
                "test", QualityType.Gut, 2, 0, DateTime.Now, DatabaseUtilities.GetSeedingCars_without_Product());
        }

        public static List<Product> GetSeedingProductsList()
        {
            return new List<Product>()
            {
                new Product(1, Guid.NewGuid(), "test", 50.0m, GetSeedingCatagory_withoutTopCat(), "test",
                "test", QualityType.Gut, 2, 0, DateTime.Now, DatabaseUtilities.GetSeedingCars_without_Product()),

                new Product(2, Guid.NewGuid(), "test2", 50.0m, GetSeedingCatagory_withoutTopCat(), "test2",
                "test", QualityType.Gut, 2, 0, DateTime.Now, DatabaseUtilities.GetSeedingCars_without_Product()),

            };

        }
        public static Catagory GetSeedingCatagory_withoutTopCat()
        {
            return new Catagory()
            {
                Name = "Test",
                Description = "Test",
                CategoryType = CategoryTypes.Auspuff,
            };
        }

        public static Catagory GetSeedingCatagory_with_TopCat()
        {
            Catagory c = new Catagory()
            {
                Name = "Test",
                Description = "Test",
                CategoryType = CategoryTypes.Fahrwerk,
            };

            return new Catagory()
            {
                Name = "Test2",
                Description = "Test2",
                CategoryType = CategoryTypes.Fahrwerk,
                TopCatagory = c
            };
        }

        public static User GetSeedingUser()
        {
            return new User()
            {
                Guid = Guid.NewGuid(),
                Nachname = "Test",
                Addrese = "Test",
                Email = "Test",
                PW = "Test",
                Vorname = "Test",
                Telefon = "Test",
                Confirmed = false,
                Role = Roles.User
            };
        }

        public static ShoppingCartItem GetSeedingShoppingCartItem()
        {
            ShoppingCart sc = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                UserNav = GetSeedingUser()
            };

            return new ShoppingCartItem()
            {
                guid = Guid.NewGuid(),
                Pieces = 2,
                ProductNav = GetSeedingProduct(),
                ShoppingCartNav = sc
            };
        }

        public static ShoppingCart GetSeedingShoppingCart()
        {
            ShoppingCart sc = new ShoppingCart()
            {
                guid = Guid.NewGuid(),
                UserNav = GetSeedingUser()
            };
            sc.AddShoppingCartItem(GetSeedingShoppingCartItem());
            return sc;
        }
    }
}
