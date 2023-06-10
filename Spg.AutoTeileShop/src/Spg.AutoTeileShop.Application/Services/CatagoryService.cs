using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Application.Services
{
    public class CatagoryService : IReadOnlyCatagoryService, IAddUpdateableCatagoryService, IDeletableCatagoryService
    {
        private readonly ICatagoryRepository _catagoryRepository;

        public CatagoryService(ICatagoryRepository catagoryRepository)
        {
            _catagoryRepository = catagoryRepository;
        }

        public Catagory AddCatagory(Catagory catagory)
        {
            return _catagoryRepository.AddCatagory(catagory);
        }

        public void DeleteCatagory(Catagory catagory)
        {
            _catagoryRepository.DeleteCatagory(catagory);
        }

        public IEnumerable<Catagory> GetAllCatagories()
        {
            return _catagoryRepository.GetAllCatagories();
        }

        public Catagory GetCatagoryById(int id)
        {
            return _catagoryRepository.GetCatagoryById(id);
        }

        public Catagory GetCatagoryByName(string name)
        {
            return _catagoryRepository.GetCatagoryByName(name);
        }

        public IEnumerable<Catagory> GetCatagoriesByTopCatagory(Catagory topCatagory)
        {
            return _catagoryRepository.GetCatagoriesByTopCatagory(topCatagory);
        }

        public IEnumerable<Catagory> GetCatagoriesByType(CategoryTypes categoryType)
        {
            return _catagoryRepository.GetCatagoriesByType(categoryType);
        }

        public string GetCatagoryDescriptionById(int id)
        {
            return _catagoryRepository.GetCatagoryDescriptionById(id);
        }

        public Catagory UpdateCatagory(int Id, Catagory catagory)
        {
            Catagory updateCatagory = _catagoryRepository.GetCatagoryById(Id);

            updateCatagory.TopCatagory = catagory.TopCatagory;
            updateCatagory.Name = catagory.Name;
            updateCatagory.Description = catagory.Description;
            updateCatagory.CategoryType = catagory.CategoryType;

            return _catagoryRepository.UpdateCatagory(catagory);
        }

        public IEnumerable<Catagory> GetCatagoriesByTopCatagoryandByType(Catagory topCatagory, CategoryTypes categoryType)
        {
            return _catagoryRepository.GetCatagoriesByTopCatagory(topCatagory).Where(c => c.CategoryType == categoryType);
        }
    }

}
