using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Domain.Exeptions
{
    public class RepositoryDeleteException : Exception
    {
        public RepositoryDeleteException()
        {
            // Logging
        }

        public RepositoryDeleteException(string message)
            : base(message)
        { }

        public RepositoryDeleteException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}
