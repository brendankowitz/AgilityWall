using PortableTrello.Contracts;

namespace AgilityWall.Core.Messages
{
    public class MoveCardToColumn
    {
        public Card Card { get; set; }
        public string DestinationColumn { get; set; }

        public MoveCardToColumn(Card card, string destinationColumn)
        {
            Card = card;
            DestinationColumn = destinationColumn;
        }
    }
}