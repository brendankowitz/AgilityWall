using System.Collections.Generic;
using AgilityWall.TrelloApi.Client.Parameters;
using AgilityWall.TrelloApi.Contracts;

namespace AgilityWall.TrelloApi.Client.Requests
{
    public class GetCardsByBoardId : ITrelloRequest<GetCardsByBoardId, IEnumerable<Card>>
    {
        public GetCardsByBoardId(string boardId, FilterOptions options = FilterOptions.all)
        {
            Resource = string.Format("/boards/{0}/cards/{1}", boardId, options);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}