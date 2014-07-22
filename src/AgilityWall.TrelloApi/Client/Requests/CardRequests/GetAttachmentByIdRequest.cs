using System.Collections.Generic;
using PortableTrello.Contracts;

namespace PortableTrello.Client.Requests.CardRequests
{
    public class GetAttachmentByIdRequest : ITrelloRequest<GetAttachmentByIdRequest, Attachment>
    {
        public GetAttachmentByIdRequest(string cardId, string attachmentId)
        {
            Resource = ResourcePathFor.Card(cardId, "attachments", attachmentId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}