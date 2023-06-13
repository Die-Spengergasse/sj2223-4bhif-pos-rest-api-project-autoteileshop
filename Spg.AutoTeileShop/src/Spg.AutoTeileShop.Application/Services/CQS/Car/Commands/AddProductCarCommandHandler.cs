using Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces;
using Spg.AutoTeileShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Application.Services.CQS.Car.Commands
{
    public class AddProductCarCommandHandler : ICommandHandler<AddProductCarCommand, Spg.AutoTeileShop.Domain.Models.Car>
    {
        private readonly IReadOnlyRepositoryBase<Product> _repoReadOnlyProduct;
        private readonly IReadOnlyRepositoryBase<Domain.Models.Car> _repoReadOnlyCar;

        private readonly IRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> _repo;


        public AddProductCarCommandHandler(IRepositoryBase<Spg.AutoTeileShop.Domain.Models.Car> repo, IReadOnlyRepositoryBase<Product> repoReadOnly, IReadOnlyRepositoryBase<Domain.Models.Car> repoReadOnlyCar)
        {
            _repo = repo;
            _repoReadOnlyProduct = repoReadOnly;
            _repoReadOnlyCar = repoReadOnlyCar;
        }

        public async Task<Domain.Models.Car> HandleAsync(AddProductCarCommand command)
        {
            Product p = _repoReadOnlyProduct.GetById(command.ProductId);
            Domain.Models.Car car = _repoReadOnlyCar.GetById(command.CarId);

            car.AddFitsForProducts(p);
            return await _repo.Update<int>(car.Id ,car);
        }
    }
}
