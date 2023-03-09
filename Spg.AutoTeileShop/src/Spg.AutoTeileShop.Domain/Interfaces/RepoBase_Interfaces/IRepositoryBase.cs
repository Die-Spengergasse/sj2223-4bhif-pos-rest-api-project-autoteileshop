using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        TEntity? Create(TEntity entity);
        TEntity? Update(TEntity entity);
        TEntity? Delete(TEntity entity);
    }
}
