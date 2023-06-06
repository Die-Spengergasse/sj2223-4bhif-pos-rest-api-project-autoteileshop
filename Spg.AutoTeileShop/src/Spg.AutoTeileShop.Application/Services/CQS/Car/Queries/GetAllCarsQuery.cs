using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Queries
{
    public class GetAllCarsQuery : IRequest<IQueryable<Spg.AutoTeileShop.Domain.Models.Car>>
    {
        public Expression<Func<Spg.AutoTeileShop.Domain.Models.Car, bool>>? Filter { get; set; }
        public Expression<Func<Spg.AutoTeileShop.Domain.Models.Car, object>>? SortBy { get; set; }
        public bool SortDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCarsQuery()
        {
            PageSize = 10;
        }
    }
}
