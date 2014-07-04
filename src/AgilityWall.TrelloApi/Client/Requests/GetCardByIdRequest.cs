using System.Collections.Generic;
using AgilityWall.TrelloApi.Contracts;

namespace AgilityWall.TrelloApi.Client.Requests
{
    public class GetCardByIdRequest : ITrelloRequest<GetCardByIdRequest, Card>
    {
        public GetCardByIdRequest(string cardId)
        {
            Resource = string.Format("/cards/{0}", cardId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}