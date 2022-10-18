using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.RequestBodyModels
{
    public class CarReqeustBodyModel
    {
        public int Id { get; set; }
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }

        public CarReqeustBodyModel(Car car)
        {
            this.Id = car.Id;
            this.Marke = car.Marke;
            this.Modell = car.Modell;
            this.Baujahr = car.Baujahr;
        }
    }
}
