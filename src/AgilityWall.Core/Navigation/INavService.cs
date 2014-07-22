using System.Collections.Generic;

namespace AgilityWall.Core.Navigation
{
    public interface INavService
    {
        bool CanGoBack { get; }

        bool Navigate<T>();

        bool Navigate<T>(object parameter);

        bool Navigate<T>(IDictionary<string, string> parameters);

        void GoForward();

        void GoBack();
    }
}