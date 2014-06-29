﻿using System.Linq;
using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class ListSummaryViewModel : PropertyChangedBase
    {
        public ListSummaryViewModel(List list)
        {
            List = list;
            Cards = new BindableCollection<CardSummaryViewModel>(list.Cards.Select(x => new CardSummaryViewModel(x)));
        }

        public List List { get; set; }
        public IObservableCollection<CardSummaryViewModel> Cards { get; set; }
    }
}