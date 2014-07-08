using System.Collections.Generic;
using PortableTrello.Client.Fields;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.MemberRequests
{
    public class GetMemberFieldByIdRequest : ITrelloRequest<GetMemberFieldByIdRequest, FieldValue>
    {
        public GetMemberFieldByIdRequest(string memberId, MemberFields field)
        {
            Resource = ResourcePathFor.Member(memberId, field.ToString());
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}