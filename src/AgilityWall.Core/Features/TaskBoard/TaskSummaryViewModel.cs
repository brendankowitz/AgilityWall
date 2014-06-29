using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class TaskSummaryViewModel : PropertyChangedBase
    {
        public TaskSummaryViewModel(Card card)
        {
            Card = card;
        }

        public Card Card { get; set; }
    }
}