using System;
using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PortableTrello.Client;
using PortableTrello.Contracts;
using PortableTrello.Contracts.CardActions;
using PropertyChanged;

namespace AgilityWall.Core.Features.CardDetails
{
    [ImplementPropertyChanged]
    public class CardDetailsViewModel : Screen, IHandle<Refresh>
    {
        private readonly ITrelloClient _trelloClient;
        private readonly CardActionsViewModel.Factory _actionFactory;
        private string _cardId;
        private readonly TaskCompletionSource<bool> _viewReady = new TaskCompletionSource<bool>();

        public CardDetailsViewModel(ITrelloClient trelloClient, CardActionsViewModel.Factory actionFactory)
        {
            _trelloClient = trelloClient;
            _actionFactory = actionFactory;
            CardActions = new BindableCollection<CardActionsViewModel>();
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
                    var coverPhotoTask = Task.Run(async () =>
                    {
                        if (!string.IsNullOrEmpty(Card.IdAttachmentCover) && Card.Badges.Attachments > 0)
                            CoverAttachment = await _trelloClient.GetAttachmentById(CardId, Card.IdAttachmentCover);
                    });

                    await Task.WhenAll(_trelloClient.GetListById(Card.IdList).ContinueWith(x => List = x.Result),
                        _trelloClient.GetCardActionsByCardId(CardId).ContinueWith(actions => CardActions.AddRange(actions.Result.Select(y => _actionFactory.Invoke(y)))),
                        coverPhotoTask);
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
        public IObservableCollection<CardActionsViewModel> CardActions { get; set; }
        public bool IsLoading { get; set; }

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
            CardActions.Clear();
        }

        public void Handle(Refresh message)
        {
            if(IsActive) OnInitialize();
        }
    }
}