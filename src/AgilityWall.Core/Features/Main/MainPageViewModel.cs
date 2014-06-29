using System.Linq;
using AgilityWall.Core.Features.Authentication;
using AgilityWall.Core.Infrastructure;
using AgilityWall.TrelloApi.Client;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.Main
{
    [ImplementPropertyChanged]
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly TrelloClient _trelloClient;

        public MainPageViewModel(INavigationService navigationService, TrelloClient trelloClient)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            Boards = new BindableCollection<BoardSummaryViewModel>();
        }

        public IObservableCollection<BoardSummaryViewModel> Boards { get; set; }
        public bool RequiredLogin { get; set; }

        protected async override void OnActivate()
        {
            if ((await _trelloClient.Initialize()))
            {
                RequiredLogin = false;
                var boards = await _trelloClient.GetBoardsForMe();
                Boards.Clear();
                Boards.AddRange(
                    boards.Where(x => !x.Closed).OrderBy(x => x.Name).Select(x => new BoardSummaryViewModel(x)));
            }
            else
                RequiredLogin = true;
        }

        public void ConnectWithTrello()
        {
            _navigationService.Navigate<AuthenticateViewModel>();
        }

        public void CreateBoard()
        {

        }
    }
}