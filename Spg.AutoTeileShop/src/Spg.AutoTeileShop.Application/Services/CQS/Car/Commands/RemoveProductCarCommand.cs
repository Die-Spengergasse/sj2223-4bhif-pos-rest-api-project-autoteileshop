using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class RemoveProductCarCommand : IRequest<Spg.AutoTeileShop.Domain.Models.Car>
    {
        public int ProductId { get; set; }
        public int CarId { get; set; }

        public RemoveProductCarCommand(int productId, int carId)
        {
            ProductId = productId;
            CarId = carId;
        }
    }
}
