using System;

namespace PortableTrello.Client
{
    public class TrelloToken
    {
        public TrelloToken(string token, DateTime? expires)
        {
            Value = token;
            Expires = expires;
        }

        public TrelloToken()
        {
        }

        public string Value { get; set; }
        public DateTime? Expires { get; set; }
    }
}