﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        void Create(TEntity newEntity);

        void Update<TKey>(TKey id, TEntity newEntity);

        void Delete<TKey>(TKey id);
    }
}
