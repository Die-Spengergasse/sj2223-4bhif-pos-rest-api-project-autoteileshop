﻿using Spg.AutoTeileShop.Infrastructure;

namespace Spg.OAuth.API.Helper
{
    public class IProfileService
    {
        protected readonly AutoTeileShopContext _db;
        
        public IProfileService(AutoTeileShopContext db)
        {
            _db = db;
        }
        
    }
}
