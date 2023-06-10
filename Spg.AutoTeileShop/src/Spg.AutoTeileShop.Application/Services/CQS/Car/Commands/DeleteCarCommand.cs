using MediatR;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class DeleteCarCommand : IRequest<Spg.AutoTeileShop.Domain.Models.Car>
    {
        public Spg.AutoTeileShop.Domain.Models.Car Car { get; set; } = default!;

        public DeleteCarCommand(Spg.AutoTeileShop.Domain.Models.Car car)
        {
            Car = car;
        }
    }
}
