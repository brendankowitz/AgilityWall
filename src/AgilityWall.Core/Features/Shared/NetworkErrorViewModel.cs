using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.Shared
{
    [ImplementPropertyChanged]
    public class NetworkErrorViewModel : PropertyChangedBase, IHandle<NetworkFailure>
    {
        private readonly IEventAggregator _eventAggregator;

        public NetworkErrorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool IsVisible { get; set; }

        public void Handle(NetworkFailure message)
        {
            IsVisible = true;
        }

        public void Retry()
        {
            IsVisible = false;
            _eventAggregator.Publish(new Refresh(), Execute.BeginOnUIThread);
        }
    }
}