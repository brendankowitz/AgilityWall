using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.BoardRequests
{
    public class GetBoardByIdRequest : ITrelloRequest<GetBoardByIdRequest, Board>
    {
        public GetBoardByIdRequest(string boardId)
        {
            Resource = ResourcePathFor.Board(boardId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}