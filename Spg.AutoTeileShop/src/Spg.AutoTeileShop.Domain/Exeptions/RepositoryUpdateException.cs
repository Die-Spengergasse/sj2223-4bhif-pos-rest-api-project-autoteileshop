namespace Spg.AutoTeileShop.Domain.Exeptions
{
    public class RepositoryUpdateException : Exception
    {
        public RepositoryUpdateException()
        {
            // Logging
        }

        public RepositoryUpdateException(string message)
            : base(message)
        { }

        public RepositoryUpdateException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}

