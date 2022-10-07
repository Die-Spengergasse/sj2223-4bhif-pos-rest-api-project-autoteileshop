using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Repository.Base;
using Spg.AutoTeileShop.Domain.Models;
{

}

namespace Spg.AutoTeileShop.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersByCityAsync(string city);
    }
    
    
}
