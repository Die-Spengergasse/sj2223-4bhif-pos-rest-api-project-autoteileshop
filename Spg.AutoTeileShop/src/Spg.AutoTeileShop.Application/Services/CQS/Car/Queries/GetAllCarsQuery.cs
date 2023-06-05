using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetAllCarsQuery : IRequest<IQueryable<Spg.AutoTeileShop.Domain.Models.Car>>
    {

        public GetAllCarsQuery()
        {
        }
    }
}
