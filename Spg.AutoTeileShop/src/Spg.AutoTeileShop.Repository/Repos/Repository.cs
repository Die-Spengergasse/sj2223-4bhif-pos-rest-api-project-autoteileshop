using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository.Base;

namespace Spg.AutoTeileShop.Repository.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AutoTeileShopContext _autoTeileShopContext;

        public Repository(AutoTeileShopContext autoTeileShopContext)
        {
            this._autoTeileShopContext = autoTeileShopContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            _autoTeileShopContext.Set<T>().Add(entity);
            await _autoTeileShopContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _autoTeileShopContext.Set<T>().Remove(entity);
            await _autoTeileShopContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
             return await _autoTeileShopContext.Set<T>().ToListAsync();
        }

             

        public async Task<T> UpdateAsync(T entity)
        {
            _autoTeileShopContext.Set<T>().Update(entity);
             await _autoTeileShopContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _autoTeileShopContext.Set<T>().FindAsync(id);
        }
    }

}
