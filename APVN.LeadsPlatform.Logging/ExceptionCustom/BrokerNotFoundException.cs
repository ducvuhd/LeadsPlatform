namespace APVN.LeadsPlatform.Logging.ExceptionCustom
{
    public class BrokerNotFoundException : System.Exception
    {
        public BrokerNotFoundException(string message) : base(message)
        {
        }

        public BrokerNotFoundException() : base()
        {
        }

        public BrokerNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
