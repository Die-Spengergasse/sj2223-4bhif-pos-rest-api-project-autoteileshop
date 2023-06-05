using MediatR;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Exeptions;
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
    public class GetAllCarsQueryHandler : IQueryHandler<GetAllCarsQuery, IQueryable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly IReadOnlyRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repository;

        public GetAllCarsQueryHandler(IReadOnlyRepositoryBase<Domain.Models.Car> repository)
        {
            _repository = repository;
        }
        public Expression<Func<Product, bool>>? Filter { get; set; }

        public async Task<IQueryable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetAllCarsQuery request)
        {
            return await Task.Run(() => _repository.GetAll())
                ?? throw new CarNotFoundException("Kein Car gefunden");
            
        }
    }
}
