using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests
{
    public class GetBoardByIdRequest : ITrelloRequest<GetBoardByIdRequest, Board>
    {
        public GetBoardByIdRequest(string boardId)
        {
            Resource = string.Format("/boards/{0}", boardId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}