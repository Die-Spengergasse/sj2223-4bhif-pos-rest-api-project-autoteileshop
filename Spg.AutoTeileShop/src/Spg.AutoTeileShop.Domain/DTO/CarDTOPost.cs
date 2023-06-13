namespace Spg.AutoTeileShop.Domain.DTO
{
    public class CarDTOPost
    {
        public string Marke { get; set; } = string.Empty;
        public string Modell { get; set; } = string.Empty;
        public DateTime Baujahr { get; set; }


        public CarDTOPost()
        {
        }
    }
}
