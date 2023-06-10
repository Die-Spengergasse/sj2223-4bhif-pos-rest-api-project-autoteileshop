using Spg.AutoTeileShop.Domain.Exeptions;
using Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase_Old<TEntity> where TEntity : class
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