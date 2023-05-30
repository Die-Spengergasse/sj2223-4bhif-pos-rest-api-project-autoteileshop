using MediatR;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Spg.AutoTeileShop.Domain.Models.Car>
    {
        private readonly IReadOnlyRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repository;

        public GetCarByIdQueryHandler(IReadOnlyRepositoryBase<Domain.Models.Car> repository)
        {
            _repository = repository;
        }
        public Expression<Func<Product, bool>>? Filter { get; set; }

        public async Task<Spg.AutoTeileShop.Domain.Models.Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _repository.GetById<int>(request.Id))
                ?? throw new Exception("Customer konnte nicht gefunden werden!");
        }
    }
}
