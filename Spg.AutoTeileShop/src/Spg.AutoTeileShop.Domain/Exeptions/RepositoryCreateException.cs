﻿namespace Spg.AutoTeileShop.Domain.Exeptions
{
    public class RepositoryCreateException : Exception
    {
        public RepositoryCreateException()
        {
            // Logging
        }

        public RepositoryCreateException(string message)
            : base(message)
        { }

        public RepositoryCreateException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}
