using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgilityWall.Core.Features.CardDetails;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.Messages;
using AgilityWall.TrelloApi.Client;
using AgilityWall.TrelloApi.Contracts;
using Caliburn.Micro;
using Microsoft.Phone.Shell;

namespace AgilityWall.WinPhone.Infrastructure.PlatformServices
{
    public class TileService : IHandleWithTask<PinCardMessage>
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
            var uri = _navigationWrapper.BuildUri(typeof (CardDetailsViewModel),
                new Dictionary<string, string>
                {
                    { "CardId", message.Card.Id },
                    {"DisplayName", message.Card.Name}
                });

            Attachment backgroundImage = null;
            if (!string.IsNullOrEmpty(message.Card.IdAttachmentCover))
                backgroundImage = await _client.GetAttachmentById(message.Card.Id, message.Card.IdAttachmentCover);

            var tileData = new StandardTileData
            {
                Title = message.Card.Name,
                BackgroundImage = backgroundImage != null ? new Uri(backgroundImage.Url) : null,
                BackContent = message.Card.Desc
            };

            ShellTile.Create(uri, tileData);
        }
    }
}