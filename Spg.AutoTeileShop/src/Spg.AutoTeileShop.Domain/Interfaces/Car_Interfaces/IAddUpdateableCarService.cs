using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces
{
    public interface IAddUpdateableCarService
    {
        Car? Add(Car car);
        Car? Update(Car car);
    }
}
