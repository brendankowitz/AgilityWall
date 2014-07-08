namespace PortableTrello.Contracts
{
    public class Badges
    {
        public int Votes { get; set; }
        public bool ViewingMemberVoted { get; set; }
        public bool Subscribed { get; set; }
        public string Fogbugz { get; set; }
        public int CheckItems { get; set; }
        public int CheckItemsChecked { get; set; }
        public int Comments { get; set; }
        public int Attachments { get; set; }
        public bool Description { get; set; }
        public string Due { get; set; }
    }
}