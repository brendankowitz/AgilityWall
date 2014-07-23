using System.Linq;
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
                if (Board != null) return;
                IsLoading = true;
                if (!string.IsNullOrEmpty(BoardId))
                {
                    Board = await _trelloClient.GetBoardById(BoardId);
                    var lists =
                        await _trelloClient.GetListsByBoardId(BoardId, ListFilterOptions.open, FilterOptions.open);

                    Items.AddRange(lists.Select(x =>
                    {
                        var y = x;
                        return _listSummaryFactory(y);
                    }));
                }
            }
            finally
            {
                IsLoading = false;
            }
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