using System.Windows.Input;
using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;

namespace AgilityWall.WinStore.Infrastructure
{
    public class NavigationHelper
    {
        readonly ICommand _goBack = new ActionCommand(_ => IoC.Get<INavigationService>().GoBack(), _ => IoC.Get<INavigationService>().CanGoBack);
        public ICommand GoBackCommand
        {
            get { return _goBack; }
        }
    }
}