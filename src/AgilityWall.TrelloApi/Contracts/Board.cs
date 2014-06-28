using System.Collections.Generic;

namespace AgilityWall.TrelloApi.Contracts
{
    public class Board
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DescData DescData { get; set; }
        public bool Closed { get; set; }
        public string IdOrganization { get; set; }
        public bool Invited { get; set; }
        public bool Pinned { get; set; }
        public bool Starred { get; set; }
        public string Url { get; set; }
        public Prefs Prefs { get; set; }
        public List<object> Invitations { get; set; }
        public List<Membership> Memberships { get; set; }
        public string ShortLink { get; set; }
        public bool Subscribed { get; set; }
        public LabelNames LabelNames { get; set; }
        public List<object> PowerUps { get; set; }
        public string DateLastActivity { get; set; }
        public string DateLastView { get; set; }
        public string ShortUrl { get; set; }
    }
}