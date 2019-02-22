using System;
using System.Collections.Generic;
using System.Text;

namespace Divayo.Core.Api
{
    public class ErrorObject
    {
        private ErrorObject(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public string ErrorCode { get; private set; }
        public string Message { get; private set; }

        public static ErrorObject CreateError(string errorCode, string message)
        {
            return new ErrorObject(errorCode, message);
        }
    }
}
