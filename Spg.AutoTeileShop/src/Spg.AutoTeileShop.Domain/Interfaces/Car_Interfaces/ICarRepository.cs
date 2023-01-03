using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        IEnumerable<Car> GetByMarke(string marke);
        IEnumerable<Car> GetByModell(string model);
        IEnumerable<Car> GetByBauJahr(DateTime baujahr);
        IEnumerable<Car> GetByMarkeAndModell(string marke, string model);
        IEnumerable<Car> GetByMarkeAndModellAndBaujahr(string marke, string model, DateTime baujahr);
        Car? GetById(int Id);
        Car? Add(Car car);
        Car? Update(Car car);
        Car? Delete(Car car);
    }
}
