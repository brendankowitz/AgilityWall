using AgilityWall.Core.Messages;
using AgilityWall.TrelloApi.Client.Requests;
using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class CardSummaryViewModel : PropertyChangedBase, IHandle<ModelResponse<GetAttachmentByIdRequest, Attachment>>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly GetAttachmentByIdRequest _getAttachmentByIdRequest;

        public CardSummaryViewModel(Card card, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Card = card;
            _getAttachmentByIdRequest = new GetAttachmentByIdRequest(Card.Id, Card.IdAttachmentCover);
            _eventAggregator.Subscribe(this);
            Initialize();
        }

        protected void Initialize()
        {
            if (!string.IsNullOrEmpty(Card.IdAttachmentCover))
                _eventAggregator.Publish(_getAttachmentByIdRequest, Execute.BeginOnUIThread);
        }

        public Card Card { get; set; }
        public Attachment CoverAttachment { get; set; }

        [DependsOn("Card")]
        public bool HasDescription
        {
            get
            {
                if (Card == null) return false;
                return !string.IsNullOrEmpty(Card.Desc);
            }
        }


        public void Handle(ModelResponse<GetAttachmentByIdRequest, Attachment> message)
        {
            if (message.RequestedResource == _getAttachmentByIdRequest.Resource)
            {
                CoverAttachment = message.Value;
            }
        }
    }
}