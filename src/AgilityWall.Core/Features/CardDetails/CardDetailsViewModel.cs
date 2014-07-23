using System;
using System.Threading.Tasks;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PortableTrello.Client;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.CardDetails
{
    [ImplementPropertyChanged]
    public class CardDetailsViewModel : Screen
    {
        private readonly TrelloClient _trelloClient;
        private readonly IEventAggregator _eventAggregator;
        private string _cardId;
        private readonly TaskCompletionSource<bool> _viewReady = new TaskCompletionSource<bool>();

        public CardDetailsViewModel(TrelloClient trelloClient, IEventAggregator eventAggregator)
        {
            _trelloClient = trelloClient;
            _eventAggregator = eventAggregator;
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();
            await _trelloClient.Initialize();

            try
            {
                await _viewReady.Task;
                IsLoading = true;
                if (!string.IsNullOrEmpty(CardId))
                {
                    Card = await _trelloClient.GetCardById(CardId);
                    if (!string.IsNullOrEmpty(Card.IdAttachmentCover))
                        CoverAttachment = await _trelloClient.GetAttachmentById(CardId, Card.IdAttachmentCover);
                    List = await _trelloClient.GetListById(Card.IdList);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override void OnViewReady(object view)
        {
            if (!_viewReady.Task.IsCompleted) _viewReady.SetResult(true);
        }

        public string CardId
        {
            get { return _cardId; }
            set
            {
                if (value == _cardId) return;
                _cardId = value;
                Reset();
                NotifyOfPropertyChange(() => CardId);
            }
        }

        public Attachment CoverAttachment { get; set; }
        public Card Card { get; set; }
        public List List { get; set; }
        public bool IsLoading { get; set; }
        public object Parameter { set { this.SetPropertiesFromNavigationParameter(value); } }

        [DependsOn("CoverAttachment")]
        public Uri CoverPhoto
        {
            get {
                if (CoverAttachment != null)
                    return new Uri(CoverAttachment.Url, UriKind.Absolute);
                return null;
            }
        }

        private void Reset()
        {
            Card = null;
            List = null;
        }

        public void Pin()
        {
            _eventAggregator.Publish(new PinCardMessage(Card), Execute.BeginOnUIThread);
        }
    }
}