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

        protected async override void OnViewAttached(object view, object context)
        {
            if (!(await _trelloClient.Initialize()))
                _navigationService.Navigate<AuthenticateViewModel>();

            var boards = await _trelloClient.GetBoardsForMe();
            Boards.Clear();
            Boards.AddRange(boards.Select(x => new BoardSummaryViewModel(x)));
        }

        public void CreateBoard()
        {

        }
    }
}