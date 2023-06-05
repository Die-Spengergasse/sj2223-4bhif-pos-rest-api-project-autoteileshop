using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarsByMarkeQuery : IRequest<IEnumerable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public string Marke { get; set; }

        public GetCarsByMarkeQuery(string marke)
        {
            Marke = marke;
        }
    }
}
