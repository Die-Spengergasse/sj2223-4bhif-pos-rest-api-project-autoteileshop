using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, int>
    {
        private readonly IRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repo;

        public DeleteCarCommandHandler(IRepositoryBase<Domain.Models.Car> repo)
        {
            _repo = repo;
        }

        public async Task<int> HandleAsync(DeleteCarCommand command)
        {
            return await _repo.Delete<int>(command.Car.Id);
        }

    }
}
