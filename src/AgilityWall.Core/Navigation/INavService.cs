namespace AgilityWall.Core.Navigation
{
    public interface INavService
    {
        bool CanGoBack { get; }

        bool Navigate<T>();

        bool Navigate<T>(object parameter);

        void GoForward();

        void GoBack();
    }
}