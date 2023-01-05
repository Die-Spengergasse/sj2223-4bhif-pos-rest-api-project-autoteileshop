using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
