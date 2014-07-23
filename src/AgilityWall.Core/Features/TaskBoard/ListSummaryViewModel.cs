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
        public delegate ListSummaryViewModel Factory(List list);
        public ListSummaryViewModel(List list, CardSummaryViewModel.Factory cardFactory)
        {
            List = list;
            Cards = new BindableCollection<CardSummaryViewModel>(list.Cards.Select(x => cardFactory(x)));
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