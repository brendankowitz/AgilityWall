using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests
{
    public class GetMemberById : ITrelloRequest<GetMemberById, Member>
    {
        public GetMemberById(string memberId)
        {
            Resource = string.Format("/members/{0}", memberId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; }}
    }
}