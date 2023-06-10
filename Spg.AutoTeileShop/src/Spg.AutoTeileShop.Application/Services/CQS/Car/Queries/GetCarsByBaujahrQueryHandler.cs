using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByBaujahrQueryHandler : IQueryHandler<GetCarsByBaujahrQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly ICarRepositoryCustom _repository;

        public GetCarsByBaujahrQueryHandler(ICarRepositoryCustom repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetCarsByBaujahrQuery request)
        {
            var cars = await Task.Run(() => _repository.GetByBauJahr(request.Baujahr));
            return cars;

        }
    }
}

