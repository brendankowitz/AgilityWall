using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.BoardRequests
{
    public class GetBoardsForMemberRequest : ITrelloRequest<GetBoardsForMemberRequest, IEnumerable<Board>>
    {
        public GetBoardsForMemberRequest(string memberId)
        {
            Resource = ResourcePathFor.Member(memberId, "boards");
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}