using System;

namespace AgilityWall.TrelloApi.Client
{
    public class TrelloToken
    {
        public TrelloToken(string token, DateTime? expires)
        {
            Token = token;
            Expires = expires;
        }

        public TrelloToken()
        {
        }

        public string Token { get; set; }
        public DateTime? Expires { get; set; }
    }
}