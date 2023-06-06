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
        //public Expression<Func<Product, bool>>? Filter { get; set; }

        public async Task<IQueryable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetAllCarsQuery request)
        {
            var query = _repository.GetAll();

            // Filtern
            if (request.Filter != null)
                query = (Task<IQueryable<Domain.Models.Car>>)query.Result.Where(request.Filter);

            // Sortieren
            if (request.SortBy != null)
            {
                if (request.SortDescending)
                    query = (Task<IQueryable<Domain.Models.Car>>)(IQueryable<Domain.Models.Car>)query.Result.OrderByDescending(request.SortBy);
                else
                    query= (Task<IQueryable<Domain.Models.Car>>)query.Result.OrderBy(request.SortBy);
            }

            // Paginieren
            query = (Task<IQueryable<Domain.Models.Car>>)query.Result.Skip((request.PageNumber - 1) * request.PageSize)
                         .Take(request.PageSize);

            return (IQueryable<Domain.Models.Car>)(await Task.FromResult(query)
                       ?? throw new CarNotFoundException("Kein Car gefunden"));
        }
    }
}
