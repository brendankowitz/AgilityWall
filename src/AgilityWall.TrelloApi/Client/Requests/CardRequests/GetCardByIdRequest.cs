using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.CardRequests
{
    public class GetCardByIdRequest : ITrelloRequest<GetCardByIdRequest, Card>
    {
        public GetCardByIdRequest(string cardId)
        {
            Resource = ResourcePathFor.Card(cardId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}