using AgilityWall.Core.Navigation;
using Caliburn.Micro;
using PortableTrello.Authentication;
using PortableTrello.Client;

namespace AgilityWall.Core.Features.Authentication
{
    public class AuthenticateViewModel : Screen
    {
        private readonly INavService _navigationService;
        private readonly ITrelloClient _trelloClient;

        public AuthenticateViewModel(INavService navigationService, ITrelloClient trelloClient)
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
                if(_navigationService.CanGoBack)
                    _navigationService.GoBack();
            }
        }
    }
}