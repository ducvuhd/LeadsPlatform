﻿using System;
using System.Collections.Generic;
using System.Net;

namespace APVN.LeadsPlatform.Logging.ExceptionCustom
{
    public abstract class BaseCustomException : Exception
    {
        public BaseCustomException(List<string> messages, HttpStatusCode statusCode)
        {
            Messages = messages;
            StatusCode = statusCode;
        }

        public List<string> Messages { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public object AdditionalData { get; set; }
    }
}
