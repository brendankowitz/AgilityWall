namespace PortableTrello.Contracts.CardActions
{
    public class CardActionData
    {
        public Board Board { get; set; }
        public Card Card { get; set; }
        public string Text { get; set; }
    }
}