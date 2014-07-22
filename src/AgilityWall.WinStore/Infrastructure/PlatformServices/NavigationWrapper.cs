using System.Collections.Generic;
using AgilityWall.Core.Navigation;
using Caliburn.Micro;

namespace AgilityWall.WinStore.Infrastructure.PlatformServices
{
    public class NavigationWrapper : INavService
    {
        private readonly Caliburn.Micro.INavigationService _navigationService;

        public NavigationWrapper(Caliburn.Micro.INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanGoBack { get { return _navigationService.CanGoBack; } }

        public bool Navigate<T>()
        {
            return Navigate<T>(null);
        }

        public bool Navigate<T>(object parameter)
        {
            return _navigationService.NavigateToViewModel<T>(parameter);
        }

        public bool Navigate<T>(IDictionary<string,string> parameters)
        {
            return Navigate<T>(parameters.ToExpando());
        }

        public void GoForward()
        {
            _navigationService.GoForward();
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}