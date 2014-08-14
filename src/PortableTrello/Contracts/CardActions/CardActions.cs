namespace PortableTrello.Contracts.CardActions
{
    public class CardAction
    {
        public string Id { get; set; }
        public string IdMemberCreator { get; set; }
        public CardActionData Data { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public CardActionMemberCreator MemberCreator { get; set; }
    }
}