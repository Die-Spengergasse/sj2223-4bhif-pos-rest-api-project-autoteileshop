using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Repository2.CustomGenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeQueryHandler : IQueryHandler<GetCarsByMarkeQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly CarRepositoryCustom _repository;

        public GetCarsByMarkeQueryHandler(CarRepositoryCustom repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetCarsByMarkeQuery request)
        {
            var cars = await Task.Run(() => _repository.GetByMarke(request.Marke));
            return cars;
        }
    }
}
