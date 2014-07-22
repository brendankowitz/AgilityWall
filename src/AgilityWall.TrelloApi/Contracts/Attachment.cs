using System.Collections.Generic;

namespace PortableTrello.Contracts
{
    public class Attachment
    {
        public string Id { get; set; }
        public int Bytes { get; set; }
        public string Date { get; set; }
        public object EdgeColor { get; set; }
        public string IdMember { get; set; }
        public bool IsUpload { get; set; }
        public object MimeType { get; set; }
        public string Name { get; set; }
        public List<AttachmentPreview> Previews { get; set; }
        public string Url { get; set; }
    }
}