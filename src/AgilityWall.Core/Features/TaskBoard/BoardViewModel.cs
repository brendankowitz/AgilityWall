﻿using System.Linq;
using AgilityWall.Core.Features.CardDetails;
using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;
using PortableTrello.Client;
using PortableTrello.Client.Parameters;
using PortableTrello.Contracts;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class BoardViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly INavigationService _navigationService;
        private readonly TrelloClient _trelloClient;
        private readonly IEventAggregator _eventAggregator;

        public BoardViewModel(INavigationService navigationService, TrelloClient trelloClient, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            _eventAggregator = eventAggregator;
        }

        public string BoardId { get; set; }
        public Board Board { get; set; }
        public bool IsLoading { get; set; }

        protected async override void OnInitialize()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(BoardId))
                {
                    Board = await _trelloClient.GetBoardById(BoardId);
                    var lists =
                        await _trelloClient.GetListsByBoardId(BoardId, ListFilterOptions.open, FilterOptions.open);

                    Items.AddRange(lists.Select(x => new ListSummaryViewModel(x, _eventAggregator)));
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void NavigateToCard(CardSummaryViewModel card)
        {
            _navigationService.Navigate<CardDetailsViewModel>(new {CardId = card.Card.Id, DisplayName = card.Card.Name});
        }

        public void CardHeld(CardSummaryViewModel card)
        {
            card.ChangeEditState();
        }

        protected override void ChangeActiveItem(object newItem, bool closePrevious)
        {
            base.ChangeActiveItem(BindingWorkaroundExtensions.EnsureModel<ListSummaryViewModel>(newItem), closePrevious);
        }
    }
}