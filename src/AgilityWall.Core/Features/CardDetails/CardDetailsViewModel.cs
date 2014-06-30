﻿using System;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using AgilityWall.TrelloApi.Client;
using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.CardDetails
{
    [ImplementPropertyChanged]
    public class CardDetailsViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly TrelloClient _trelloClient;
        private readonly IEventAggregator _eventAggregator;

        public CardDetailsViewModel(INavigationService navigationService, TrelloClient trelloClient, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            _eventAggregator = eventAggregator;
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            await _trelloClient.Initialize();

            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(CardId))
                {
                    Card = await _trelloClient.GetCardId(CardId);
                    if (!string.IsNullOrEmpty(Card.IdAttachmentCover))
                        CoverAttachment = await _trelloClient.GetAttachment(CardId, Card.IdAttachmentCover);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public Attachment CoverAttachment { get; set; }
        public Card Card { get; set; }
        public string CardId { get; set; }
        public bool IsLoading { get; set; }

        [DependsOn("CoverAttachment")]
        public Uri CoverPhoto
        {
            get {
                if (CoverAttachment != null)
                    return new Uri(CoverAttachment.Url);
                return new Uri("/Assets/RandomBg3.jpg", UriKind.Relative);
            }
        }

        public void Pin()
        {
            _eventAggregator.Publish(new PinCardMessage(Card), Execute.BeginOnUIThread);
        }
    }
}