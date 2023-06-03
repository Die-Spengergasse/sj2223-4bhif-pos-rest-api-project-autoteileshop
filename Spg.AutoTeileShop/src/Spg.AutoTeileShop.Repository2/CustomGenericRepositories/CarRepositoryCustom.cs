using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Repository2.CustomGenericRepositories
{
    public class CarRepositoryCustom
    {
        private AutoTeileShopContext _db { get; set; }
        public CarRepositoryCustom(AutoTeileShopContext context)
        {
            _db = context;
        }
        public IEnumerable<Car> GetByBauJahr(DateTime baujahr)
        {
            return _db.Cars.Where(c => c.Baujahr.Year == baujahr.Year).ToList();
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

        public IEnumerable<Car> GetByFitProduct(Product product)
        {
            var cars = _db.Cars.Include(c => c.FitsForProducts).ToList();
            var result = cars.Where(c => c.FitsForProducts.Contains(product)).ToList();
            return result;
        }

    }
}
