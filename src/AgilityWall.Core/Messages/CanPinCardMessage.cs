using System;
using PortableTrello.Contracts;

namespace AgilityWall.Core.Messages
{
    public class CanPinCardMessage
    {
        private readonly Action<bool> _result;

        public CanPinCardMessage(Card card, Action<bool> result)
        {
            _result = result;
            Card = card;
        }

        public Card Card { get; protected set; }

        public void SetResult(bool result)
        {
            _result(result);
        }
    }
}