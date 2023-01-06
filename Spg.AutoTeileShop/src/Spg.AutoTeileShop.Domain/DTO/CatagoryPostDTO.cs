﻿using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class CatagoryPostDTO
    {
        public CatagoryPostDTO()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryTypes CategoryType { get; set; }
        public int TopCatagoryId { get; set; }
        
    }
    
}
