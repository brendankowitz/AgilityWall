using System.Collections.Generic;

namespace AgilityWall.TrelloApi.Contracts
{
    public class Card
    {
        public string Id { get; set; }
        public List<object> CheckItemStates { get; set; }
        public bool Closed { get; set; }
        public string DateLastActivity { get; set; }
        public string Desc { get; set; }
        public object DescData { get; set; }
        public string IdBoard { get; set; }
        public string IdList { get; set; }
        public List<object> IdMembersVoted { get; set; }
        public int IdShort { get; set; }
        public object IdAttachmentCover { get; set; }
        public bool ManualCoverAttachment { get; set; }
        public string Name { get; set; }
        public double Pos { get; set; }
        public string ShortLink { get; set; }
        public Badges Badges { get; set; }
        public object Due { get; set; }
        public List<object> IdChecklists { get; set; }
        public List<object> IdMembers { get; set; }
        public List<object> Labels { get; set; }
        public string ShortUrl { get; set; }
        public bool Subscribed { get; set; }
        public string Url { get; set; }
    }
}