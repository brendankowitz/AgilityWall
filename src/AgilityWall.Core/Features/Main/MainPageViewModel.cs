using System.Linq;
using AgilityWall.Core.Features.About;
using AgilityWall.Core.Features.Authentication;
using AgilityWall.Core.Features.TaskBoard;
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
        public bool IsLoading { get; set; }

        protected async override void OnInitialize()
        {
            try
            {
                IsLoading = true;
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
            finally
            {
                IsLoading = false;
            }
        }

        public void ConnectWithTrello()
        {
            _navigationService.Navigate<AuthenticateViewModel>();
        }

        public void NavigateToBoard(BoardSummaryViewModel viewModel)
        {
            _navigationService.Navigate<BoardViewModel>(new { BoardId = viewModel.Board.Id, DisplayName = viewModel.Board.Name });
        }

        public void NavigateToAbout()
        {
            _navigationService.Navigate<AboutViewModel>();
        }

        public void CreateBoard()
        {

        }
    }
}