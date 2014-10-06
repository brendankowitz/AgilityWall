using System;
using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Features.TaskBoard;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using PortableTrello.Client;
using PortableTrello.Contracts;

namespace AgilityWall.WinPhone.Infrastructure.PlatformServices
{
    public class TileService : IHandleWithTask<PinBoardMessage>,
        IHandle<CanPinBoardMessage>
    {
        private readonly INavigationService _navigationService;

        public TileService(INavigationService navigationService, ITrelloClient client)
        {
            _navigationService = navigationService;
        }

        public async Task Handle(PinBoardMessage message)
        {
            var uri = GetBoardUri(message.Board);
            var board = message.Board;

            var tileData = new StandardTileData
            {
                Title = board.Name,
                BackgroundImage = new Uri("/Assets/Tiles/BoardTileMedium.png", UriKind.Relative),
            };
            if (!string.IsNullOrEmpty(board.Desc))
            {
                tileData.BackContent = board.Desc;
            }

            ShellTile.Create(uri, tileData);
        }

        private Uri GetBoardUri(Board board)
        {
            var uri = _navigationService.UriFor<BoardViewModel>()
                .WithParam(x => x.BoardId, board.Id)
                .WithParam(x => x.DisplayName, board.Name)
                .BuildUri();
            return uri;
        }

        public void Handle(CanPinBoardMessage message)
        {
            var uri = GetBoardUri(message.Board);
            message.SetResult(ShellTile.ActiveTiles.All(x => x.NavigationUri != uri));
        }
    }
}