using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces;
using Spg.AutoTeileShop.Domain.Exeptions;

namespace Spg.AutoTeileShop.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    {
        protected readonly AutoTeileShopContext _db;

        public RepositoryBase(AutoTeileShopContext db)
        {
            _db = db;
        }

        public TEntity? Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new RepositoryCreateException($"{nameof(TEntity)} war NULL!");
            }
            
            _db.Set<TEntity>().Add(entity);
            
            try
            {
                _db.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                throw new RepositoryCreateException($"{nameof(TEntity)} konnte nicht gespeichert werden!", e);
            }
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