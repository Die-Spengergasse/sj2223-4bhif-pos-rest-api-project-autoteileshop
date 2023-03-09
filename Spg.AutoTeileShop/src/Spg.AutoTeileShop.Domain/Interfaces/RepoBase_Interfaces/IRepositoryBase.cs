using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces
{
    internal interface IRepositoryBasey<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int Id);
        TEntity? Create(TEntity entity);
        TEntity? Update(TEntity entity);
        TEntity? Delete(TEntity entity);
    }
}
