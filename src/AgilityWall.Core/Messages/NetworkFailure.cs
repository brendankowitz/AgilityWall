using System;

namespace AgilityWall.Core.Messages
{
    public class NetworkFailure
    {
        public NetworkFailure(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; set; } 
    }
}