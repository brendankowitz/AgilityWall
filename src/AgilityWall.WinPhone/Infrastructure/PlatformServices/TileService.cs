﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Features.CardDetails;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using PortableTrello.Client;
using PortableTrello.Contracts;

namespace AgilityWall.WinPhone.Infrastructure.PlatformServices
{
    public class TileService : IHandleWithTask<PinCardMessage>, IHandle<CanPinCardMessage>
    {
        private readonly NavigationWrapper _navigationWrapper;
        private readonly TrelloClient _client;

        public TileService(NavigationWrapper navigationWrapper, TrelloClient client)
        {
            _navigationWrapper = navigationWrapper;
            _client = client;
        }

        public async Task Handle(PinCardMessage message)
        {
            var card = message.Card;
            var uri = GetCardUri(card);

            Attachment backgroundImage = null;
            if (!string.IsNullOrEmpty(card.IdAttachmentCover))
                backgroundImage = await _client.GetAttachmentById(card.Id, card.IdAttachmentCover);

            var tileData = new StandardTileData
            {
                Title = card.Name,
                BackgroundImage = backgroundImage != null ? new Uri(backgroundImage.Url) : null,
                BackContent = card.Desc
            };

            ShellTile.Create(uri, tileData);
        }
        
        public void Handle(CanPinCardMessage message)
        {
            var card = message.Card;
            var uri = GetCardUri(card);

            var result = ShellTile.ActiveTiles.Any(x => x.NavigationUri == uri);
            message.SetResult(result);
        }

        private Uri GetCardUri(Card card)
        {
            var uri = _navigationWrapper.BuildUri(typeof(CardDetailsViewModel),
                new Dictionary<string, string>
                {
                    {"CardId", card.Id},
                    {"DisplayName", card.Name}
                });
            return uri;
        }
    }
}