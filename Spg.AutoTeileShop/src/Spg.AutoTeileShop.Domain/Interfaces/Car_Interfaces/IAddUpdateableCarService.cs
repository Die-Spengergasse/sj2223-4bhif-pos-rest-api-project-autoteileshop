﻿using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces
{
    public interface IAddUpdateableCarService
    {
        Car? Add(Car car);
        Car? Update(Car car);
    }
}
