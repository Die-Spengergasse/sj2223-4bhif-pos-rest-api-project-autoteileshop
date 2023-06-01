using Org.BouncyCastle.Asn1.Ocsp;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, int>
    {
        private readonly RepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repo;

        public DeleteCarCommandHandler(RepositoryBase<Domain.Models.Car> repo)
        {
            _repo = repo;
        }

        public async Task<int> HandleAsync(DeleteCarCommand command)
        {
            return await _repo.Delete<int>(command.Car.Id);
        }

    }
}
