using MediatR;

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
