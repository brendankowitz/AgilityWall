namespace AgilityWall.TrelloApi.Contracts
{
    public class List
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BoardId { get; set; }
        public Card[] Cards { get; set; }
    }
}