using System.Collections.Generic;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.ListRequests
{
    public class GetListsByBoardIdRequest : ITrelloRequest<GetListsByBoardIdRequest, IEnumerable<List>>
    {
        public GetListsByBoardIdRequest(string boardId,
            ListFilterOptions options = ListFilterOptions.all,
            FilterOptions cards = FilterOptions.none)
        {
            Resource = ResourcePathFor.Board(boardId, "lists", options.ToString());
            Parameters = new Dictionary<string, string>
            {
                {"cards", cards.ToString()}
            };
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
    }
}