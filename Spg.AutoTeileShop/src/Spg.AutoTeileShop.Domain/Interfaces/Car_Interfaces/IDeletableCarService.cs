using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces
{
    public interface IDeletableCarService
    {
        Car? Delete(Car car);
    }
}
