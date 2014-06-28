using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
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