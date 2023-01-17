using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces
{
    public interface IAddUpdateableCatagoryService
    {
        Catagory AddCatagory(Catagory catagory);
        Catagory UpdateCatagory(int Id, Catagory catagory);
    }
}
