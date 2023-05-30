using Microsoft.Extensions.DependencyInjection;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private Func<object, Mediator> value;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Mediator(Func<object, Mediator> value)
        {
            this.value = value;
        }

        public async Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command)
        {
            var handler = _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
            return await handler.HandleAsync(command);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
        {
            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query);
        }
    }
}
