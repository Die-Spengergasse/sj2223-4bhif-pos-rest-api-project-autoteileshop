using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class UpdateCarCommandHandler : ICommandHandler<UpdateCarCommand, Spg.AutoTeileShop.Domain.Models.Car>
    {
        private readonly IRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repo;

        public UpdateCarCommandHandler(IRepositoryBase<Domain.Models.Car> repo)
        {
            _repo = repo;
        }

        public async Task<Domain.Models.Car> HandleAsync(UpdateCarCommand request)
        {
            return await _repo.Update(request.Car.Id, request.Car);
        }

    }
}
