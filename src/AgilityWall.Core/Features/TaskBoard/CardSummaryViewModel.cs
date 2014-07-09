using System.Linq;
using System.Windows.Input;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PortableTrello.Client.Requests.CardRequests;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class CardSummaryViewModel : PropertyChangedBase, IHandle<ModelResponse<GetAttachmentByIdRequest, Attachment>>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAvatarUrlResolver _avatarResolver;
        private readonly GetAttachmentByIdRequest _getAttachmentByIdRequest;

        public CardSummaryViewModel(Card card, IEventAggregator eventAggregator, IAvatarUrlResolver avatarResolver)
        {
            _eventAggregator = eventAggregator;
            _avatarResolver = avatarResolver;
            Card = card;
            _getAttachmentByIdRequest = new GetAttachmentByIdRequest(Card.Id, Card.IdAttachmentCover);
            MemberAvatars = new BindableCollection<string>();
            _eventAggregator.Subscribe(this);
            MoveRight = new ActionCommand(_ => {});
            MoveLeft = new ActionCommand(_ => {});
            Initialize();
        }

        protected async void Initialize()
        {
            if (!string.IsNullOrEmpty(Card.IdAttachmentCover))
                _eventAggregator.Publish(_getAttachmentByIdRequest, Execute.BeginOnUIThread);

            if (Card.IdMembers.Any())
                MemberAvatars.AddRange(await _avatarResolver.GetTrelloMemberGravitar(56, Card.IdMembers.ToArray()));
        }

        public Card Card { get; set; }
        public Attachment CoverAttachment { get; set; }
        public CardDisplayStates State { get; set; }
        public ICommand MoveLeft { get; set; }
        public ICommand MoveRight { get; set; }
        public IObservableCollection<string> MemberAvatars { get; set; }

        [DependsOn("Card")]
        public bool HasDescription
        {
            get
            {
                if (Card == null) return false;
                return Card.Badges.Description;
            }
        }

        [DependsOn("Card")]
        public bool HasComments
        {
            get
            {
                if (Card == null) return false;
                return Card.Badges.Comments > 0;
            }
        }

        [DependsOn("Card")]
        public bool HasAttachments
        {
            get
            {
                if (Card == null) return false;
                return Card.Badges.Attachments > 0;
            }
        }

        [DependsOn("Card")]
        public bool HasLists
        {
            get
            {
                if (Card == null) return false;
                return Card.Badges.CheckItems > 0;
            }
        }

        [DependsOn("Card")]
        public bool HasDueDate
        {
            get
            {
                if (Card == null) return false;
                return !string.IsNullOrEmpty(Card.Badges.Due);
            }
        }

        [DependsOn("Card")]
        public bool HasMembers
        {
            get
            {
                if (Card == null) return false;
                return Card.IdMembers.Any();
            }
        }

        public void Handle(ModelResponse<GetAttachmentByIdRequest, Attachment> message)
        {
            if (message.RequestedResource == _getAttachmentByIdRequest.Resource)
            {
                CoverAttachment = message.Value;
            }
        }

        public void ChangeEditState()
        {
            State = State == CardDisplayStates.Moving ? 
                CardDisplayStates.Normal : CardDisplayStates.Moving;
        }
    }
}