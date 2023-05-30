using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Interfaces.Generic_Repository_Interfaces
{
    public interface IFindableByGuid
    {
        public Guid Guid { get; }
    }
}
