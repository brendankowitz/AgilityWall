using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.Shared
{
    [ImplementPropertyChanged]
    public class NetworkErrorViewModel : PropertyChangedBase, IHandle<NetworkFailure>, IHandle<AllowOfflineMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isVisible;

        public NetworkErrorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            ShowOfflineStatus = true;
        }

        [AlsoNotifyFor("IsVisible")]
        public bool ShowOfflineStatus { get; set; }

        public bool IsVisible
        {
            get { return _isVisible && ShowOfflineStatus; }
            set
            {
                if (value.Equals(_isVisible)) return;
                _isVisible = value;
                NotifyOfPropertyChange(() => IsVisible);
            }
        }

        public void Handle(NetworkFailure message)
        {
            IsVisible = true;
        }

        public void Retry()
        {
            IsVisible = false;
            _eventAggregator.Publish(new Refresh(), Execute.BeginOnUIThread);
        }

        public void Handle(AllowOfflineMessage message)
        {
            ShowOfflineStatus = message.Show;
        }
    }
}