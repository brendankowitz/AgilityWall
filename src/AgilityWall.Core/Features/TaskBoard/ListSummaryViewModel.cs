using System.Linq;
using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class ListSummaryViewModel : PropertyChangedBase
    {
        private readonly IAvatarUrlResolver _avatarResolver;

        public ListSummaryViewModel(List list, IEventAggregator eventAggregator, IAvatarUrlResolver avatarResolver)
        {
            _avatarResolver = avatarResolver;
            List = list;
            Cards = new BindableCollection<CardSummaryViewModel>(list.Cards.Select(x => new CardSummaryViewModel(x, eventAggregator, avatarResolver)));
        }

        public List List { get; set; }
        public IObservableCollection<CardSummaryViewModel> Cards { get; set; }
        [DependsOn("Cards")]
        public bool NoCards { get { return !Cards.Any(); }}

        public override bool Equals(object obj)
        {
            var model = BindingWorkaroundExtensions.EnsureModel<ListSummaryViewModel>(obj);
            return base.Equals(model);
        }
    }
}