﻿namespace Spg.AutoTeileShop.Domain.Exeptions
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
