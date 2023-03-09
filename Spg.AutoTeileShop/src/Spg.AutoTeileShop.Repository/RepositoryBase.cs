using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces;

namespace Spg.AutoTeileShop.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    {
        protected readonly AutoTeileShopContext _db;

        public TEntity? Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity? Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity? Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}