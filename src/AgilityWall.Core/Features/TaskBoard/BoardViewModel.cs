using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Features.CardDetails;
using AgilityWall.Core.Infrastructure;
using AgilityWall.TrelloApi.Client;
using AgilityWall.TrelloApi.Client.Parameters;
using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class BoardViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly TrelloClient _trelloClient;
        private readonly IEventAggregator _eventAggregator;

        public BoardViewModel(INavigationService navigationService, TrelloClient trelloClient, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            _eventAggregator = eventAggregator;
            Lists = new BindableCollection<ListSummaryViewModel>();
        }

        public string BoardId { get; set; }
        public Board Board { get; set; }
        public IObservableCollection<ListSummaryViewModel> Lists { get; set; }
        public bool IsLoading { get; set; }

        protected async override void OnInitialize()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(BoardId))
                {
                    Board = await _trelloClient.GetBoardById(BoardId);
                    var lists =
                        await _trelloClient.GetBoardListsById(BoardId, ListFilterOptions.open, FilterOptions.open);

                    Lists.AddRange(lists.Select(x => new ListSummaryViewModel(x, _eventAggregator)));
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void NavigateToCard(CardSummaryViewModel card)
        {
            _navigationService.Navigate<CardDetailsViewModel>(new {CardId = card.Card.Id, DisplayName = card.Card.Name});
        }
    }
}