using AgilityWall.Core.Messages;
using Caliburn.Micro;

namespace AgilityWall.Core.Features.About
{
    public class AboutViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public AboutViewModel(IEventAggregator eventAggregator)
        {
            DisplayName = "about";
            _eventAggregator = eventAggregator;
        }

        protected override void OnActivate()
        {
            _eventAggregator.Publish(new AllowOfflineMessage(false), Execute.BeginOnUIThread);
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Publish(new AllowOfflineMessage(true), Execute.BeginOnUIThread);
        }
    }
}