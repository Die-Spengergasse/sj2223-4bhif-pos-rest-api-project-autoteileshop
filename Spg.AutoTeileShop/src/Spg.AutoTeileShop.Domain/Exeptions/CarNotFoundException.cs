namespace Spg.AutoTeileShop.Domain.Exeptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException()
        {
            // Logging
        }

        public CarNotFoundException(string message)
            : base(message)
        { }

        public CarNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}
