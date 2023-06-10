using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces
{
    public interface IReadOnlyCarService
    {
        IEnumerable<Car> GetAll();
        IEnumerable<Car> GetByBauJahr(DateTime baujahr);
        Car? GetById(int Id);
        IEnumerable<Car> GetByMarke(string marke);
        IEnumerable<Car> GetByModell(string model);
        IEnumerable<Car> GetByMarkeAndModell(string marke, string model);
        IEnumerable<Car> GetByMarkeAndModellAndBaujahr(string marke, string model, DateTime baujahr);
    }
}
