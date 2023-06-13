using AutoMapper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.ApplicationTest.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Car, CarDTO>();
            CreateMap<CarDTO, Car>();
           // CreateMap<Car, CarDTOPost>();
            CreateMap<CarDTOPost, Car>();
            //CreateMap<Car, CarDTOUpdate>();
            CreateMap<CarDTOUpdate, Car>();
        }
    }
}
