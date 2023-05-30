using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class CreateCarCommand : IRequest<Spg.AutoTeileShop.Domain.Models.Car>
    {
        public Spg.AutoTeileShop.Domain.Models.Car Car { get; set; } = default!;

        public CreateCarCommand(Spg.AutoTeileShop.Domain.Models.Car car)
        {
            Car = car;
        }
    }
}
