using PortableTrello.Contracts;

namespace AgilityWall.Core.Messages
{
    public class PinCardMessage
    {
        public PinCardMessage(Card card)
        {
            Card = card;
        }

        public Card Card { get; protected set; } 
    }
}