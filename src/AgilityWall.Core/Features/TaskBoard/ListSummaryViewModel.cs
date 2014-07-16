using System.Linq;
using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class ListSummaryViewModel : PropertyChangedBase, IHaveDisplayName
    {
        public ListSummaryViewModel(List list, IEventAggregator eventAggregator, IAvatarUrlResolver avatarResolver)
        {
            List = list;
            Cards = new BindableCollection<CardSummaryViewModel>(list.Cards.Select(x => new CardSummaryViewModel(x, eventAggregator, avatarResolver)));
            DisplayName = list.Name;
        }

        public List List { get; set; }
        public IObservableCollection<CardSummaryViewModel> Cards { get; set; }
        [DependsOn("Cards")]
        public bool NoCards { get { return !Cards.Any(); }}
        public string DisplayName { get; set; }

        public override bool Equals(object obj)
        {
            var model = BindingWorkaroundExtensions.EnsureModel<ListSummaryViewModel>(obj);
            return base.Equals(model);
        }   
    }
}