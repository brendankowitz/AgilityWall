using System.Collections.Generic;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests
{
    public class GetCardsByBoardIdRequest : ITrelloRequest<GetCardsByBoardIdRequest, IEnumerable<Card>>
    {
        public GetCardsByBoardIdRequest(string boardId, FilterOptions options = FilterOptions.all)
        {
            Resource = string.Format("/boards/{0}/cards/{1}", boardId, options);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}