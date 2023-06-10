namespace Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity> Create(TEntity newEntity);

        Task<TEntity> Update<TKey>(TKey id, TEntity newEntity);

        Task<TKey> Delete<TKey>(TKey id);
    }
}
