using System.Collections.Generic;
using AgilityWall.TrelloApi.Client.Parameters;
using AgilityWall.TrelloApi.Contracts;

namespace AgilityWall.TrelloApi.Client.Requests
{
    public class GetListsByBoardIdRequest : ITrelloRequest<GetListsByBoardIdRequest, IEnumerable<List>>
    {
        public GetListsByBoardIdRequest(string boardId,
            ListFilterOptions options = ListFilterOptions.all,
            FilterOptions cards = FilterOptions.none)
        {
            Resource = string.Format("/boards/{0}/lists/{1}", boardId, options);
            Parameters = new Dictionary<string, string>
            {
                {"cards", cards.ToString()}
            };
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
    }
}