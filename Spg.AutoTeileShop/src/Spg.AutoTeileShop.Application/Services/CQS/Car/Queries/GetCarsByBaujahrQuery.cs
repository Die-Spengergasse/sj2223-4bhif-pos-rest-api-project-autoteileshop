using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByBaujahrQuery : IRequest<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public DateTime Baujahr { get; set; }

        public GetCarsByBaujahrQuery(DateTime baujahr)
        {
            Baujahr = baujahr;
        }
    }
}
