using System.Collections.Generic;
using PortableTrello.Client.Fields;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.MemberRequests
{
    public class GetMemberById : ITrelloRequest<GetMemberById, Member>
    {
        public GetMemberById(string memberId, MemberFields fields = MemberFields.all)
        {
            Resource = ResourcePathFor.Member(memberId);
            Parameters = new Dictionary<string, string>
            {
                {"fields", fields.ToString()}
            };
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get; set; }
    }
}