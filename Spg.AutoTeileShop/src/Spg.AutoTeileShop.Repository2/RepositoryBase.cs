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

        public async Task<TEntity> Create(TEntity newEntity)
        {
            if (newEntity is null) throw new RepositoryCreateException($"{nameof(TEntity)} war NULL!");

            await _db.AddAsync(newEntity);
            try
            {
                await _db.SaveChangesAsync();
                return newEntity;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryCreateException($"Create ist für {nameof(TEntity)} fehlgeschlagen!", ex);
            }
        }

        public async Task<TEntity> Delete<TKey>(TKey id)
        {
            _db.Set<TEntity>().Remove(_db.Set<TEntity>().Find(id) ?? throw new RepositoryDeleteException("Objekt nicht gefunden."));
            await _db.SaveChangesAsync();
            return _db.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> Update<TKey>(TKey id, TEntity newEntity)
        {
            if (newEntity == null)
            {
                throw new RepositoryUpdateException("Objekt ist null");
            }

            var existingEntity = await _db.Set<TEntity>().FindAsync(id);

            if (existingEntity != null)
            {
                _db.Entry(existingEntity).CurrentValues.SetValues(newEntity);
                await _db.SaveChangesAsync();
            }

            return existingEntity;
        }

    }
}
