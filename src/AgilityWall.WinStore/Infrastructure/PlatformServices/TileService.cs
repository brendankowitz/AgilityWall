using System.Threading.Tasks;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PortableTrello.Client;

namespace AgilityWall.WinStore.Infrastructure.PlatformServices
{
    public class TileService : IHandleWithTask<PinBoardMessage>
    {
        private readonly ITrelloClient _client;

        public TileService(ITrelloClient client)
        {
            _client = client;
        }

        public async Task Handle(PinBoardMessage message)
        {
            //var uri = _navigationWrapper.BuildUri(typeof (CardDetailsViewModel),
            //    new Dictionary<string, string>
            //    {
            //        { "CardId", message.Card.Id },
            //        {"DisplayName", message.Card.Name}
            //    });

            //Attachment backgroundImage = null;
            //if (!string.IsNullOrEmpty(message.Card.IdAttachmentCover))
            //    backgroundImage = await _client.GetAttachmentById(message.Card.Id, message.Card.IdAttachmentCover);

            //var tileData = new StandardTileData
            //{
            //    Title = message.Card.Name,
            //    BackgroundImage = backgroundImage != null ? new Uri(backgroundImage.Url) : null,
            //    BackContent = message.Card.Desc
            //};

            //ShellTile.Create(uri, tileData);
        }
    }
}