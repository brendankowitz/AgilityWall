using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Features.CardDetails;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Navigation;
using Caliburn.Micro;
using PortableTrello.Client;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class BoardViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly INavService _navigationService;
        private readonly TrelloClient _trelloClient;
        private readonly ListSummaryViewModel.Factory _listSummaryFactory;
        private string _boardId;
        private readonly TaskCompletionSource<bool> _viewReady = new TaskCompletionSource<bool>();

        public BoardViewModel(INavService navigationService, TrelloClient trelloClient,
            ListSummaryViewModel.Factory listSummaryFactory)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
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

        public object Parameter { set { this.SetPropertiesFromNavigationParameter(value); } }

        public Board Board { get; set; }
        public bool IsLoading { get; set; }

        protected async override void OnActivate()
        {
            try
            {
                if (Board != null || string.IsNullOrEmpty(BoardId)) return;
                await _viewReady.Task;

                IsLoading = true;

                Board = await _trelloClient.GetBoardById(BoardId);
                var lists = await _trelloClient.GetListsByBoardId(BoardId, ListFilterOptions.open, FilterOptions.open);

                Items.AddRange(lists.Select(x => _listSummaryFactory(x)));
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
    }
}