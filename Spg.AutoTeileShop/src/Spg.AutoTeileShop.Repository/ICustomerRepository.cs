using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AutoTeileShop.Repository.Base;
using Spg.AutoTeileShop.Domain.Models;


namespace Spg.AutoTeileShop.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByIdAsync(int id);
    }
    
    
}
