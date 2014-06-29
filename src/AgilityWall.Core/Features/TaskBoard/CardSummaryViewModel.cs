using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class CardSummaryViewModel : PropertyChangedBase
    {
        public Card Card { get; set; }

        public CardSummaryViewModel(Card card)
        {
            Card = card;
        }
    }
}