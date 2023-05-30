using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces
{
    public interface IMediator
    {
        Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command);
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query);
    }

}
