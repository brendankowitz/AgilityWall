using System;
using System.Collections.Generic;
using System.Linq;
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
            string parameterString = null;
            if(parameters != null && parameters.Any())
                parameterString = "caliburn://nav?" + string.Join("&", parameters.Select(x => string.Format("{0}={1}", Uri.EscapeUriString(x.Key), Uri.EscapeUriString(x.Value))));
            return Navigate<T>(parameterString);
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