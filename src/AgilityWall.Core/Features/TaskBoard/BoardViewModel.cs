using System.Linq;
using System.Threading.Tasks;
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
        public IObservableCollection<TaskSummaryViewModel> Cards { get; set; }

        public BoardViewModel(INavigationService navigationService, TrelloClient trelloClient)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            Cards = new BindableCollection<TaskSummaryViewModel>();
        }

        protected async override void OnInitialize()
        {
            if (!string.IsNullOrEmpty(BoardId))
            {
                Board = await _trelloClient.GetBoardById(BoardId);
                var cards = await _trelloClient.GetBoardCards(BoardId, GetCardOptions.visible);

                Cards.AddRange(cards.Select(x => new TaskSummaryViewModel(x)));
            }
        }
    }
}