using System.Collections.Generic;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.ListRequests
{
    public class GetListByIdRequest : ITrelloRequest<GetListByIdRequest, List>
    {
        public GetListByIdRequest(string idList, FilterOptions cards = FilterOptions.none)
        {
            Resource = ResourcePathFor.List(idList);
            Parameters = new Dictionary<string, string>
            {
                {"cards", cards.ToString()}
            };
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
    }
}