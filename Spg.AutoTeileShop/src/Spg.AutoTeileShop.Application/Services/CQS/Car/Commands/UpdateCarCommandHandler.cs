﻿using MediatR;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class UpdateCarCommandHandler : ICommandHandler<UpdateCarCommand, Spg.AutoTeileShop.Domain.Models.Car>
    {
        private readonly RepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repo;

        public UpdateCarCommandHandler(RepositoryBase<Domain.Models.Car> repo)
        {
            _repo = repo;
        }

        public async Task<Domain.Models.Car> HandleAsync(UpdateCarCommand request)
        {
            return await _repo.Update(request.Car.Id, request.Car);
        }

    }
}