﻿using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Exeptions;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Linq.Expressions;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarByIdQueryHandler : IQueryHandler<GetCarByIdQuery, Spg.AutoTeileShop.Domain.Models.Car>
    {
        private readonly IReadOnlyRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repository;

        public GetCarByIdQueryHandler(IReadOnlyRepositoryBase<Domain.Models.Car> repository)
        {
            _repository = repository;
        }
        public Expression<Func<Product, bool>>? Filter { get; set; }

        public async Task<Spg.AutoTeileShop.Domain.Models.Car> HandleAsync(GetCarByIdQuery request)
        {
            Spg.AutoTeileShop.Domain.Models.Car c = await Task.Run(() => _repository.GetById<int>(request.Id))
                ?? throw new CarNotFoundException("Car konnte nicht gefunden werden!");
            return c;
        }
    }
}
