using Caliburn.Micro;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.Main
{
    [ImplementPropertyChanged]
    public class BoardSummaryViewModel : PropertyChangedBase
    {
        public Board Board { get; set; }

        public BoardSummaryViewModel(Board board)
        {
            Board = board;
        }
    }
}