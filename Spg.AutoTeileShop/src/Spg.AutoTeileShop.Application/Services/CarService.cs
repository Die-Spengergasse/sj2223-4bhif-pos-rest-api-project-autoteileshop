using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services
{
    public class CarService : IReadOnlyCarService, IDeletableCarService, IAddUpdateableCarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public Car? Add(Car car)
        {
            return _carRepository.Add(car);
        }

        public Car? Delete(Car car)
        {
            return _carRepository.Delete(car);
        }

        public IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public IEnumerable<Car> GetByBauJahr(DateTime baujahr)
        {
            return _carRepository.GetByBauJahr(baujahr);
        }

        public Car? GetById(int Id)
        {
            return _carRepository.GetById(Id);
        }

        public IEnumerable<Car> GetByMarke(string marke)
        {
            return _carRepository.GetByMarke(marke);
        }

        public IEnumerable<Car> GetByMarkeAndModell(string marke, string model)
        {
            return _carRepository.GetByMarkeAndModell(marke, model);
        }

        public IEnumerable<Car> GetByMarkeAndModellAndBaujahr(string marke, string model, DateTime baujahr)
        {
            return _carRepository.GetByMarkeAndModellAndBaujahr(marke, model, baujahr);
        }

        public IEnumerable<Car> GetByModell(string model)
        {
            return _carRepository.GetByModell(model);
        }

        public Car? Update(Car car)
        {
            var car2 = _carRepository.GetById(car.Id);
            car2.Baujahr = car.Baujahr;
            car2.Modell = car.Modell;
            car2.Marke = car.Marke;

            foreach (Product p in car.FitsForProducts)
            {
                if (!car2.FitsForProducts.Contains(p)) 
                {
                    car2.AddFitsForProducts(p);
                }
            }
            foreach (Product p in car2.FitsForProducts)
            {
                if (!car.FitsForProducts.Contains(p))
                {
                    car2.RemoveFitsForProducts(p);
                }
            }
            return _carRepository.Update(car);
        }
    }
}
