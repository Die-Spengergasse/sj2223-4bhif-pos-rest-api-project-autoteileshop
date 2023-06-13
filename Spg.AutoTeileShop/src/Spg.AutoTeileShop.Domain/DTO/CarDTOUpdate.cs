using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class CarDTOUpdate
    {
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }

        public CarDTOUpdate(Car car)
        {
            this.Marke = car.Marke;
            this.Modell = car.Modell;
            this.Baujahr = car.Baujahr;
        }

        public CarDTOUpdate()
        {
        }
    }
}
