using System;
using PortableTrello.Contracts;

namespace AgilityWall.Core.Messages
{
    public class CanPinBoardMessage
    {
        private readonly Action<bool> _result;

        public CanPinBoardMessage(Board board, Action<bool> result)
        {
            _result = result;
            Board = board;
        }

        public Board Board { get; protected set; }

        public void SetResult(bool result)
        {
            _result.Invoke(result);
        }
    }
}