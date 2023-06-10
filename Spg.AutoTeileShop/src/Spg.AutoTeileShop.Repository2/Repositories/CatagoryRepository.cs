using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class CatagoryRepository : ICatagoryRepository
    {
        private readonly AutoTeileShopContext _db;

        public CatagoryRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public Catagory AddCatagory(Catagory catagory)
        {
            _db.Catagories.Add(catagory);
            _db.SaveChanges();
            return catagory;
        }

        public void DeleteCatagory(Catagory catagory)
        {
            _db.Catagories.Remove(catagory);
            _db.SaveChanges();
        }

        public IEnumerable<Catagory> GetAllCatagories()
        {
            return _db.Catagories.ToList();
        }

        public IEnumerable<Catagory> GetCatagoriesByTopCatagory(Catagory topCatagory)
        {
            return _db.Catagories.Where(c => c.TopCatagory == topCatagory).ToList();
        }

        public IEnumerable<Catagory> GetCatagoriesByType(CategoryTypes categoryType)
        {
            return _db.Catagories.Where(c => c.CategoryType == categoryType).ToList();
        }

        public Catagory GetCatagoryById(int id)
        {
            return _db.Catagories.Find(id) ?? throw new Exception($"Catagory with Id: {id} not found");
        }

        public Catagory GetCatagoryByName(string name)
        {
            return _db.Catagories.Where(c => c.Name == name).SingleOrDefault() ?? throw new Exception($"Catagory with Name: {name} not found");

        }

        public string GetCatagoryDescriptionById(int id)
        {
            return _db.Catagories.Find(id)?.Description ?? throw new Exception($"Catagory with Id: {id} not found");
        }

        public Catagory UpdateCatagory(Catagory catagory)
        {
            _db.Catagories.Update(catagory);
            _db.SaveChanges();
            return catagory;
        }
    }
}
