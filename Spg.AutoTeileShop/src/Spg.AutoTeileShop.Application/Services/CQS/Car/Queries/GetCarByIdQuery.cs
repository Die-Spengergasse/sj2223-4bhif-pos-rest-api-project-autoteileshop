using MediatR;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetCarByIdQuery : IRequest<Spg.AutoTeileShop.Domain.Models.Car>
    {
        public int Id { get; set; }

        public GetCarByIdQuery(int id)
        {
            Id = id;
        }
    }
}
