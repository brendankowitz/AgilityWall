namespace AgilityWall.TrelloApi.Contracts
{
    public class Card
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string ListId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}