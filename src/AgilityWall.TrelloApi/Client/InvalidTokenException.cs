using System;

namespace PortableTrello.Client
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }
}