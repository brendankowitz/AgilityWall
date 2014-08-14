using System.Collections.Generic;
using PortableTrello.Contracts.CardActions;

namespace PortableTrello.Client.Requests.CardRequests
{
    public class GetCardActionsByCardIdRequest : ITrelloRequest<GetCardActionsByCardIdRequest, CardAction[]>
    {
        public GetCardActionsByCardIdRequest(string cardId)
        {
            Resource = ResourcePathFor.Card(cardId, "actions");
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}