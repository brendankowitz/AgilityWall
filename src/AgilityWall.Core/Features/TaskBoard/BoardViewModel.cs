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

        public string BoardId { get; set; }
        public Board Board { get; set; }
        public IObservableCollection<ListSummaryViewModel> Lists { get; set; }

        public BoardViewModel(INavigationService navigationService, TrelloClient trelloClient)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            Lists = new BindableCollection<ListSummaryViewModel>();
        }

        protected async override void OnInitialize()
        {
            if (!string.IsNullOrEmpty(BoardId))
            {
                Board = await _trelloClient.GetBoardById(BoardId);
                var cards = await _trelloClient.GetBoardListsById(BoardId, ListFilterOptions.open, FilterOptions.open);

                Lists.AddRange(cards.Select(x => new ListSummaryViewModel(x)));
            }
        }

        public void NavigateToCard(CardSummaryViewModel card)
        {
            _navigationService.Navigate<CardDetailsViewModel>(new {CardId = card.Card.Id, DisplayName = card.Card.Name});
        }
    }
}