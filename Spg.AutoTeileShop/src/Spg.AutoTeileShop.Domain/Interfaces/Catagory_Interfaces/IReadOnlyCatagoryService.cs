using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces
{
    public interface IReadOnlyCatagoryService
    {
        Catagory GetCatagoryById(int id);
        Catagory GetCatagoryByName(string name);
        string GetCatagoryDescriptionById(int id);
        IEnumerable<Catagory> GetAllCatagories();
        IEnumerable<Catagory> GetCatagoriesByType(CategoryTypes categoryType);
        IEnumerable<Catagory> GetCatagoriesByTopCatagory(Catagory topCatagory);
        IEnumerable<Catagory> GetCatagoriesByTopCatagoryandByType(Catagory topCatagory, CategoryTypes categoryType);
    }
}
