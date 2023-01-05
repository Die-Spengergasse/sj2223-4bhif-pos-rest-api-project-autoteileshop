using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces
{
    public interface ICatagoryRepository
    {
        public Catagory GetCatagoryById(int id);
        public Catagory GetCatagoryByName(string name);
        public string GetCatagoryDescriptionById(int id);
        public IEnumerable<Catagory> GetAllCatagories();
        public IEnumerable<Catagory> GetCatagoriesByType(CategoryTypes categoryType);
        public IEnumerable<Catagory> GetCatagoriesByTopCatagory(Catagory topCatagory);
        public Catagory AddCatagory(Catagory catagory);
        public Catagory UpdateCatagory(Catagory catagory);
        public void DeleteCatagory(Catagory catagory);
    }
}
