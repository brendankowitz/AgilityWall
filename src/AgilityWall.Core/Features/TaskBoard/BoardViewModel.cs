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
        private readonly IEventAggregator _eventAggregator;
        private readonly IAvatarUrlResolver _avatarUrlResolver;
        private string _boardId;

        public BoardViewModel(INavService navigationService, TrelloClient trelloClient, IEventAggregator eventAggregator, IAvatarUrlResolver avatarUrlResolver)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            _eventAggregator = eventAggregator;
            _avatarUrlResolver = avatarUrlResolver;
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

                    Items.AddRange(lists.Select(x => new ListSummaryViewModel(x, _eventAggregator, _avatarUrlResolver)));
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
            _navigationService.Navigate<CardDetailsViewModel>(new {CardId = card.Card.Id, DisplayName = card.Card.Name});
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