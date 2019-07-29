using System;

namespace together_aspcore.App.Service
{
    public class ServiceException : Exception
    {
        public ServiceErrorCode ErrorCode { get; }

        public ServiceException(ServiceErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}