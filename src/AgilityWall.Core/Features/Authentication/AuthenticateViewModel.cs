using AgilityWall.Core.Infrastructure;
using AgilityWall.TrelloApi.Authentication;
using AgilityWall.TrelloApi.Client;
using Caliburn.Micro;

namespace AgilityWall.Core.Features.Authentication
{
    public class AuthenticateViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly TrelloClient _trelloClient;

        public AuthenticateViewModel(INavigationService navigationService, TrelloClient trelloClient)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
        }

        protected async override void OnViewAttached(object view, object context)
        {
            var frame = view as IAuthenticationFrame;

            if (!await _trelloClient.Initialize())
            {
                await _trelloClient.Authenticate(frame);
                _navigationService.GoBack();
            }
        }
    }
}