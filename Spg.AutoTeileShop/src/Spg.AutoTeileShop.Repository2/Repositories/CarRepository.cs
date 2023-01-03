using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Repository2.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AutoTeileShopContext _db;
        public CarRepository(AutoTeileShopContext db)
        {
            _db = db;
        }

        public Car? Add(Car car)
        {
            if (car is not null)
            {
                _db.Cars.Add(car);
                _db.SaveChanges();
                return car;
            }
            throw new Exception("Car is null");
        }

        public Car? Delete(Car car)
        {
            if (car is not null)
            {
                _db.Cars.Remove(car);
                _db.SaveChanges();
                return car;
            }
            throw new Exception("Car is null");
        }
        public Car? Update(Car car)
        {
            if (car is not null)
            {
                _db.Cars.Update(car);
                _db.SaveChanges();
                return car;
            }
            return null;
        }

        public Car? Update2(Car car)
        {
            if (car is not null)
            {
                var car2 = GetById(car.Id);
                car2.Baujahr = car.Baujahr;
                car2.Modell = car.Modell;
                car2.Marke = car.Marke;
                _db.Cars.Update(car2);
                _db.SaveChanges();
                return car;
            }
            return null;
        }

        public Car? Update3(Car car)
        {
            if (car is not null)
            {
                var car2 = GetById(car.Id);
                car2.Baujahr = car.Baujahr;
                car2.Modell = car.Modell;
                car2.Marke = car.Marke;
                _db.SaveChanges();
                return car;
            }
            return null;
        }

        public IEnumerable<Car> GetAll()
        {
            return _db.Cars.ToList();
        }

        public IEnumerable<Car> GetByBauJahr(DateTime baujahr)
        {
            return _db.Cars.Where(c => c.Baujahr.Year == baujahr.Year).ToList();
        }

        public Car? GetById(int Id)
        {
            return _db.Cars.Find(Id) ?? throw new Exception($"No Car found with Id: {Id}");
        }

        public IEnumerable<Car> GetByMarke(string marke)
        {
            return _db.Cars.Where(c => c.Marke == marke).ToList();
        }
        public IEnumerable<Car> GetByModell(string model)
        {
            return _db.Cars.Where(c => c.Modell == model).ToList();
        }

        public IEnumerable<Car> GetByMarkeAndModell(string marke, string model)
        {
            return _db.Cars.Where(c => c.Marke == marke && c.Modell == model).ToList();
        }

        public IEnumerable<Car> GetByMarkeAndModellAndBaujahr(string marke, string model, DateTime baujahr)
        {
            return _db.Cars.Where(c => c.Marke == marke && c.Modell == model && c.Baujahr.Year == baujahr.Year).ToList();
        }        

        
    }
}
