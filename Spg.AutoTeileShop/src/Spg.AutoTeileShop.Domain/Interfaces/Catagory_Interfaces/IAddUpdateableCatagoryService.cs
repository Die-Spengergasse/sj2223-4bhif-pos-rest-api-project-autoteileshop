using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces
{
    public interface IAddUpdateableCatagoryService
    {
        Catagory AddCatagory(Catagory catagory);
        Catagory UpdateCatagory(int Id, Catagory catagory);
    }
}
