using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity> Create(TEntity newEntity);

        Task<TEntity> Update<TKey>(TKey id, TEntity newEntity);

        Task<TEntity> Delete<TKey>(TKey id);
    }
}
