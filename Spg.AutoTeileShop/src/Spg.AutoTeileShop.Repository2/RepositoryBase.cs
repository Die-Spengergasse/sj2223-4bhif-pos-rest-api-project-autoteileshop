using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.Exeptions;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Repository2
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly AutoTeileShopContext _db;

        public RepositoryBase(AutoTeileShopContext db)
        {
            _db = db;
        }

        public void Create(TEntity newEntity)
        {
            if (newEntity is null) throw new RepositoryCreateException($"{nameof(TEntity)} war NULL!");

            _db.Add(newEntity);
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryCreateException($"Create ist für {nameof(TEntity)} fehlgeschlagen!", ex);
            }
        }

        public void Delete<TKey>(TKey id)
        {
            _db.Set<TEntity>().Remove(_db.Set<TEntity>().Find(id) ?? throw new RepositoryDeleteException("Objekt nicht gefunden."));

        }

        public void Update<TKey>(TKey id, TEntity newEntity)
        {
            _db.Set<TEntity>().Update(newEntity ?? throw new RepositoryUpdateException("Objekt ist null"));
        }
    }
}
