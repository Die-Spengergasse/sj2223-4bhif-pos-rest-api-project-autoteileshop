using Spg.AutoTeileShop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Spg.AutoTeileShop.API.RequestBodyModels
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }

        public CarDTO(Car car)
        {
            this.Id = car.Id;
            this.Marke = car.Marke;
            this.Modell = car.Modell;
            this.Baujahr = car.Baujahr;
        }
    }
}
