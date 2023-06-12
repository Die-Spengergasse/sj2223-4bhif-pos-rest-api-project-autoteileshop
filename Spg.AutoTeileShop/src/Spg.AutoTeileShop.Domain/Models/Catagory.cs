using Spg.AutoTeileShop.Domain.DTO;

namespace Spg.AutoTeileShop.Domain.Models
{
    public enum CategoryTypes
    {
        MotorTeile, Bremssystem, Tuning, Optik, Fahrwerk, Antrieb, Sonstiges,
        //Sub
        Auspuff, Bremsen, Getriebe, Kupplung, Motor, Reifen, Räder, Scheibenwischer, Scheinwerfer, Sitze, Stoßdämpfer, Elektrik, Verkleidung
    }

    public class Catagory
    {
        public int Id { get; private set; }
        public int? TopCatagoryId { get; set; }
        public virtual Catagory? TopCatagory { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryTypes CategoryType { get; set; }

        public override string ToString()
        {
            string topCatagoryString = TopCatagory != null ? TopCatagory.ToString() : "null";
            return $"{{\"Id\": {Id}, \"TopCatagory\": {topCatagoryString}, \"Name\": \"{Name}\", \"Description\": \"{Description}\", \"CategoryType\": \"{CategoryType}\"}}";
        }


        public Catagory(int id, Catagory? topCatagory, string name, string description, CategoryTypes categoryType)
        {
            Id = id;
            TopCatagory = topCatagory;
            Name = name;
            Description = description;
            CategoryType = categoryType;
        }

        public Catagory(Catagory? topCatagory, string name, string description, CategoryTypes categoryType)
        {
            TopCatagory = topCatagory;
            Name = name;
            Description = description;
            CategoryType = categoryType;
        }

        public Catagory()
        {
        }

        public Catagory(CatagoryPostDTO catagoryPostDTO, Catagory topCatagory)
        {
            Name = catagoryPostDTO.Name;
            Description = catagoryPostDTO.Description;
            CategoryType = catagoryPostDTO.CategoryType;
            TopCatagory = topCatagory;
        }
    }
}
