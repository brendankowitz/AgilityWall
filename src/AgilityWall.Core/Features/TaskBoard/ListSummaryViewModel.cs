﻿using System.Linq;
using AgilityWall.Core.Infrastructure;
using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class ListSummaryViewModel : PropertyChangedBase
    {
        public ListSummaryViewModel(List list, IEventAggregator eventAggregator)
        {
            List = list;
            Cards = new BindableCollection<CardSummaryViewModel>(list.Cards.Select(x => new CardSummaryViewModel(x, eventAggregator)));
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