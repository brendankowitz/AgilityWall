using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests
{
    public class GetBoardsForUserRequest : ITrelloRequest<GetBoardsForUserRequest, IEnumerable<Board>>
    {
        public GetBoardsForUserRequest(string userId)
        {
            Resource = string.Format("/members/{0}/boards", userId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}