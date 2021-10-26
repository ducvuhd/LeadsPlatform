using APVN.LeadsPlatform.Logging.ExceptionCustom;
using System.Collections.Generic;
using System.Net;

namespace APVN.LeadsPlatform.Logging.LoggingMiddlewares
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message = "Cannot find object") : base(new List<string> { message },
            HttpStatusCode.NotFound)
        {
        }
    }
}
