﻿using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, Spg.AutoTeileShop.Domain.Models.Car>
    {
        private readonly IRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repo;

        public CreateCarCommandHandler(IRepositoryBase<Domain.Models.Car> repo)
        {
            _repo = repo;
        }

        public async Task<Domain.Models.Car> HandleAsync(CreateCarCommand request)
        {
            return await _repo.Create(request.Car);
        }

    }
}
