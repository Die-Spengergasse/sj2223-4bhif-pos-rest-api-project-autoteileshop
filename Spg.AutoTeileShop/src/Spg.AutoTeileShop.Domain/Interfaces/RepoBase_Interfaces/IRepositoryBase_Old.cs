namespace Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces
{
    public interface IRepositoryBase_Old<TEntity>
    {
        TEntity? Create(TEntity entity);
        TEntity? Update(TEntity entity);
        TEntity? Delete(TEntity entity);
    }
}
