using Spg.AutoTeileShop.Domain.DTO;
using System.Text.Json;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }
        private List<Product> _fitsForProducts { get; set; } = new();
        public virtual IReadOnlyList<Product> FitsForProducts => _fitsForProducts;

        public Car()
        {
        }



        public override string ToString()
        {
            return $"{{\"Id\": {Id}, \"Marke\": \"{Marke}\", \"Modell\": \"{Modell}\", \"Baujahr\": \"{Baujahr.ToString("yyyy-MM-dd")}\"}}";
        }


        public Car(CarDTO carDto)
        {
            this.Id = carDto.Id;
            this.Marke = carDto.Marke;
            this.Modell = carDto.Modell;
            this.Baujahr = carDto.Baujahr;
        }
        public Car(CarDTOPost carDto)
        {
            this.Marke = carDto.Marke;
            this.Modell = carDto.Modell;
            this.Baujahr = carDto.Baujahr;
        }

        public Car(int Id, CarDTOUpdate carDto)
        {
            this.Id = Id;
            this.Marke = carDto.Marke;
            this.Modell = carDto.Modell;
            this.Baujahr = carDto.Baujahr;
        }
        
        public Car(int id, string marke, string modell, DateTime baujahr, List<Product> fitsForProducts)
        {
            Id = id;
            Marke = marke;
            Modell = modell;
            Baujahr = baujahr;
            _fitsForProducts = fitsForProducts;
        }

        public Car(string marke, string modell, DateTime baujahr)
        {
            Marke = marke;
            Modell = modell;
            Baujahr = baujahr;
        }

        public void AddFitsForProducts(Product entity)
        {
            if (entity is not null)
            {
                _fitsForProducts.Add(entity);
                if (!entity.ProductFitsForCar.Contains(this))
                    entity.AddProductFitsForCar(this);
            }
        }

        public void RemoveFitsForProducts(Product entity)
        {
            if (entity is not null)
            {
                _fitsForProducts.Remove(entity);
                if (entity.ProductFitsForCar.Contains(this))
                    entity.RemoveProductFitsForCar(this);
            }

        }
    }
}
