using Spg.AutoTeileShop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class CarDTOPost
    {
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }

        //public CarDTOPost(Car car)
        //{
        //    this.Marke = car.Marke;
        //    this.Modell = car.Modell;
        //    this.Baujahr = car.Baujahr;
        //}

        public CarDTOPost()
        {
        }
    }
}
