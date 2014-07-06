﻿using System.Collections.Generic;
using AgilityWall.TrelloApi.Contracts;

namespace AgilityWall.TrelloApi.Client.Requests
{
    public class GetAttachmentByIdRequest : ITrelloRequest<GetAttachmentByIdRequest, Attachment>
    {
        public GetAttachmentByIdRequest(string cardId, string attachmentId)
        {
            Resource = string.Format("/cards/{0}/attachments/{1}", cardId, attachmentId);
        }

        public string Resource { get; private set; }
        public IDictionary<string, string> Parameters { get { return RequestDefaults.EmptyDictionary; } }
    }
}