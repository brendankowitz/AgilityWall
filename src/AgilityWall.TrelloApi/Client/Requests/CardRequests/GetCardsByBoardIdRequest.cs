using System.Collections.Generic;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.CardRequests
{
    public class GetCardsByBoardIdRequest : ITrelloRequest<GetCardsByBoardIdRequest, IEnumerable<Card>>
    {
        public GetCardsByBoardIdRequest(string boardId, FilterOptions options = FilterOptions.all)
        {
            Resource = ResourcePathFor.Board(boardId, "cards", options.ToString());
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}