using System;

namespace AgilityWall.Core.Infrastructure
{
    public interface INavigationService
    {
        bool CanGoBack { get; }

        bool Navigate<T>();

        bool Navigate<T>(object parameter);

        void GoForward();

        void GoBack(); 
    }
}