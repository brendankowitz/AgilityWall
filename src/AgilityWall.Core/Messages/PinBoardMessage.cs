using PortableTrello.Contracts;

namespace AgilityWall.Core.Messages
{
    public class PinBoardMessage
    {
        public PinBoardMessage(Board board)
        {
            Board = board;
        }

        public Board Board { get; protected set; } 
    }
}