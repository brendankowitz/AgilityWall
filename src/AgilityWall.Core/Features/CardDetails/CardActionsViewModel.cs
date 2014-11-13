using System.Linq;
using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;
using PortableTrello.Contracts.CardActions;
using PropertyChanged;

namespace AgilityWall.Core.Features.CardDetails
{
    [ImplementPropertyChanged]
    public class CardActionsViewModel : PropertyChangedBase
    {
        private readonly IAvatarUrlResolver _avatars;

        public delegate CardActionsViewModel Factory(CardAction action);
        public CardActionsViewModel(CardAction action, IAvatarUrlResolver avatars)
        {
            _avatars = avatars;
            Action = action;

            if (Action != null)
            {
                avatars.GetTrelloMemberGravitar(trelloMemberId: Action.IdMemberCreator)
                    .ContinueWith(x => AvatarUri = x.Result.FirstOrDefault());
            }
        }

        public CardAction Action { get; set; }
        public string AvatarUri { get; set; }
    }
}