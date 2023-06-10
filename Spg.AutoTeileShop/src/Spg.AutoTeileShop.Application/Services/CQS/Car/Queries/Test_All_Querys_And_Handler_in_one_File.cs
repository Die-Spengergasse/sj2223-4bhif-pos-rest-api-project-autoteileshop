using MediatR;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByModellQuery : IRequest<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public string Modell { get; set; }

        public GetCarsByModellQuery(string modell)
        {
            Modell = modell;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByModellQueryHandler : IQueryHandler<GetCarsByModellQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly ICarRepositoryCustom _repository;

        public GetCarsByModellQueryHandler(ICarRepositoryCustom repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetCarsByModellQuery request)
        {
            var cars = await Task.Run(() => _repository.GetByModell(request.Modell));
            return cars;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeAndModellQuery : IRequest<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public string Marke { get; set; }
        public string Modell { get; set; }

        public GetCarsByMarkeAndModellQuery(string marke, string modell)
        {
            Marke = marke;
            Modell = modell;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeAndModellQueryHandler : IQueryHandler<GetCarsByMarkeAndModellQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly ICarRepositoryCustom _repository;

        public GetCarsByMarkeAndModellQueryHandler(ICarRepositoryCustom repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetCarsByMarkeAndModellQuery request)
        {
            var cars = await Task.Run(() => _repository.GetByMarkeAndModell(request.Marke, request.Modell));
            return cars;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeAndModellAndBaujahrQuery : IRequest<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public string Marke { get; set; }
        public string Modell { get; set; }
        public DateTime Baujahr { get; set; }

        public GetCarsByMarkeAndModellAndBaujahrQuery(string marke, string modell, DateTime baujahr)
        {
            Marke = marke;
            Modell = modell;
            Baujahr = baujahr;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeAndModellAndBaujahrQueryHandler : IQueryHandler<GetCarsByMarkeAndModellAndBaujahrQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly ICarRepositoryCustom _repository;

        public GetCarsByMarkeAndModellAndBaujahrQueryHandler(ICarRepositoryCustom repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetCarsByMarkeAndModellAndBaujahrQuery request)
        {
            var cars = await Task.Run(() => _repository.GetByMarkeAndModellAndBaujahr(request.Marke, request.Modell, request.Baujahr));
            return cars;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByFitProductQuery : IRequest<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public Product Product { get; set; }

        public GetCarsByFitProductQuery(Product product)
        {
            Product = product;
        }
    }
}

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByFitProductQueryHandler : IQueryHandler<GetCarsByFitProductQuery, IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        private readonly ICarRepositoryCustom _repository;

        public GetCarsByFitProductQueryHandler(ICarRepositoryCustom repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>> HandleAsync(GetCarsByFitProductQuery request)
        {
            var cars = await Task.Run(() => _repository.GetByFitProduct(request.Product));
            return (IEnumerable<Domain.Models.Car>)cars;
        }
    }
}

