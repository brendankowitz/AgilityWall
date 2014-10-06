using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Features.CardDetails;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using AgilityWall.Core.Navigation;
using Caliburn.Micro;
using PortableTrello.Client;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class BoardViewModel : Conductor<object>.Collection.OneActive, IHandle<Refresh>
    {
        private readonly INavService _navigationService;
        private readonly ITrelloClient _trelloClient;
        private readonly IEventAggregator _eventAggregator;
        private readonly ListSummaryViewModel.Factory _listSummaryFactory;
        private string _boardId;
        private readonly TaskCompletionSource<bool> _viewReady = new TaskCompletionSource<bool>();
        private Task _initialiseTrelloClient = null;

        public BoardViewModel(INavService navigationService, 
            ITrelloClient trelloClient, IEventAggregator eventAggregator,
            ListSummaryViewModel.Factory listSummaryFactory)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            _eventAggregator = eventAggregator;
            _listSummaryFactory = listSummaryFactory;
        }

        public string BoardId
        {
            get { return _boardId; }
            set
            {
                if (value == _boardId) return;
                _boardId = value;
                Reset();
                NotifyOfPropertyChange(() => BoardId);
            }
        }

        public Board Board { get; set; }
        public bool IsLoading { get; set; }
        public bool CanPin { get; set; }

        protected override void OnInitialize()
        {
            _initialiseTrelloClient = _trelloClient.Initialize();
        }

        protected async override void OnActivate()
        {
            try
            {
                if (string.IsNullOrEmpty(BoardId)) return;

                await Task.WhenAll(_initialiseTrelloClient, _viewReady.Task);

                if (Board == null)
                {
                    IsLoading = true;

                    Board = await _trelloClient.GetBoardById(BoardId);
                    var lists =
                        await _trelloClient.GetListsByBoardId(BoardId, ListFilterOptions.open, FilterOptions.open);

                    Items.AddRange(lists.Select(x => _listSummaryFactory(x)));
                }

               _eventAggregator.Publish(new CanPinBoardMessage(Board, x=> CanPin = x), Execute.BeginOnUIThread);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override void OnViewReady(object view)
        {
            if(!_viewReady.Task.IsCompleted) _viewReady.SetResult(true);
        }

        private void Reset()
        {
            Board = null;
            Items.Clear();
        }

        public void NavigateToCard(CardSummaryViewModel card)
        {
            _navigationService.ForView<CardDetailsViewModel>()
                .WithParam(x => x.CardId, card.Card.Id)
                .WithParam(x => x.DisplayName, card.Card.Name)
                .Navigate();
        }

        public void CardHeld(CardSummaryViewModel card)
        {
            card.ChangeEditState();
        }

        protected override void ChangeActiveItem(object newItem, bool closePrevious)
        {
            base.ChangeActiveItem(BindingWorkaroundExtensions.EnsureModel<ListSummaryViewModel>(newItem), closePrevious);
        }

        public void Pin()
        {
            if(CanPin)
                _eventAggregator.Publish(new PinBoardMessage(Board), Execute.BeginOnUIThread);
        }

        public void Handle(Refresh message)
        {
            if (IsActive) OnActivate();
        }
    }
}