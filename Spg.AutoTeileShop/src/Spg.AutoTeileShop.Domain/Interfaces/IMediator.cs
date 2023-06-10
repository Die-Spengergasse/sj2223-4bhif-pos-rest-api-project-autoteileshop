namespace Spg.AutoTeileShop.Domain.Interfaces
{
    public interface IMediator
    {
        Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command);
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query);
    }

}
