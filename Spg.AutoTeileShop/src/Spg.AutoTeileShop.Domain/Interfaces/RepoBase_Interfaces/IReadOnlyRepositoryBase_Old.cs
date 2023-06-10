namespace Spg.AutoTeileShop.Domain.Interfaces.RepoBase_Interfaces
{
    public interface IReadOnlyRepositoryBase_Old<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int Id);
    }
}
