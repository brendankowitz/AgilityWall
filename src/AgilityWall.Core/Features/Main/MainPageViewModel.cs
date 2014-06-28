using AgilityWall.Core.Features.Authentication;
using AgilityWall.Core.Infrastructure;
using AgilityWall.TrelloApi.Client;
using Caliburn.Micro;

namespace AgilityWall.Core.Features.Main
{
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly TrelloClient _trelloClient;

        public MainPageViewModel(INavigationService navigationService, TrelloClient trelloClient)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
        }

        protected async override void OnViewAttached(object view, object context)
        {
            if (!(await _trelloClient.Initialize()))
                _navigationService.Navigate<AuthenticateViewModel>();

            var boards = await _trelloClient.GetBoardsForMe();
        }

        public void CreateBoard()
        {

        }
    }
}