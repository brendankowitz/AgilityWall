using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class CardSummaryViewModel : PropertyChangedBase
    {
        public CardSummaryViewModel(Card card)
        {
            Card = card;
        }

        public Card Card { get; set; }

        [DependsOn("Card")]
        public bool HasDescription
        {
            get
            {
                if (Card == null) return false;
                return !string.IsNullOrEmpty(Card.Desc);
            }
        }
    }
}