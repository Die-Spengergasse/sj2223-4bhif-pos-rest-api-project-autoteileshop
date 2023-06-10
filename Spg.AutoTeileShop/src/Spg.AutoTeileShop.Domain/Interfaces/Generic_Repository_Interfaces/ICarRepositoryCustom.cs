using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces
{
    public interface ICarRepositoryCustom
    {
        IEnumerable<Car> GetByBauJahr(DateTime baujahr);
        IEnumerable<Car> GetByMarke(string marke);
        IEnumerable<Car> GetByModell(string model);
        IEnumerable<Car> GetByMarkeAndModell(string marke, string model);
        IEnumerable<Car> GetByMarkeAndModellAndBaujahr(string marke, string model, DateTime baujahr);
        IEnumerable<Car> GetByFitProduct(Product product);
    }
}
