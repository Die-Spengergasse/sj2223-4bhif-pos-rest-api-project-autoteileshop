using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeQueryHandler : IQueryHandler<GetCarsByMarkeQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly ICarRepositoryCustom _repository;

        public GetCarsByMarkeQueryHandler(ICarRepositoryCustom repository)
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
