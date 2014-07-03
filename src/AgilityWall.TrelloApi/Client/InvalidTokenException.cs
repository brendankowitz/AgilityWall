using System;

namespace AgilityWall.TrelloApi.Client
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }
}