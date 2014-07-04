using System.Collections.Generic;
using AgilityWall.TrelloApi.Client.Parameters;
using AgilityWall.TrelloApi.Contracts;

namespace AgilityWall.TrelloApi.Client.Requests
{
    public class GetListByIdRequest : ITrelloRequest<GetListByIdRequest, List>
    {
        public GetListByIdRequest(string idList, FilterOptions cards = FilterOptions.none)
        {
            Resource = string.Format("/lists/{0}", idList);
            Parameters = new Dictionary<string, string>
            {
                {"cards", cards.ToString()}
            };
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
    }
}