using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;
using PortableTrello.Contracts.CardActions;
using PropertyChanged;

namespace AgilityWall.Core.Features.CardDetails
{
    [ImplementPropertyChanged]
    public class CardActionsViewModel : PropertyChangedBase
    {
        public delegate CardActionsViewModel Factory(CardAction action);
        public CardActionsViewModel(CardAction action, IAvatarUrlResolver avatars)
        {
            Action = action;
        }

        public CardAction Action { get; set; }
    }
}