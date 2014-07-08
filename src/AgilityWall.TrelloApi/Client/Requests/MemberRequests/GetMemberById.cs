using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.MemberRequests
{
    public class GetMemberById : ITrelloRequest<GetMemberById, Member>
    {
        public GetMemberById(string memberId)
        {
            Resource = ResourcePathFor.Member(memberId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; }}
    }
}