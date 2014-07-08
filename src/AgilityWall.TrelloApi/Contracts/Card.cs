using System.Collections.Generic;

namespace PortableTrello.Contracts
{
    public class Card
    {
        public string Id { get; set; }
        public List<CheckItemState> CheckItemStates { get; set; }
        public bool Closed { get; set; }
        public string DateLastActivity { get; set; }
        public string Desc { get; set; }
        public DescData DescData { get; set; }
        public string IdBoard { get; set; }
        public string IdList { get; set; }
        public List<object> IdMembersVoted { get; set; }
        public int IdShort { get; set; }
        public string IdAttachmentCover { get; set; }
        public bool ManualCoverAttachment { get; set; }
        public string Name { get; set; }
        public double Pos { get; set; }
        public string ShortLink { get; set; }
        public Badges Badges { get; set; }
        public string Due { get; set; }
        public List<string> IdChecklists { get; set; }
        public List<string> IdMembers { get; set; }
        public List<Label> Labels { get; set; }
        public string ShortUrl { get; set; }
        public bool Subscribed { get; set; }
        public string Url { get; set; }
    }
}