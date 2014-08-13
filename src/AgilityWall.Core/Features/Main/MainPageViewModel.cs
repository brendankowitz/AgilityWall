using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Features.About;
using AgilityWall.Core.Features.Authentication;
using AgilityWall.Core.Features.TaskBoard;
using AgilityWall.Core.Messages;
using AgilityWall.Core.Navigation;
using Caliburn.Micro;
using PortableTrello.Client;
using PropertyChanged;

namespace AgilityWall.Core.Features.Main
{
    [ImplementPropertyChanged]
    public class MainPageViewModel : Screen, IHandle<Refresh>
    {
        private readonly INavService _navigationService;
        private readonly ITrelloClient _trelloClient;
        private readonly TaskCompletionSource<bool> _viewReady = new TaskCompletionSource<bool>();

        public MainPageViewModel(INavService navigationService, ITrelloClient trelloClient)
        {
            _navigationService = navigationService;
            _trelloClient = trelloClient;
            Boards = new BindableCollection<BoardSummaryViewModel>();
        }

        public IObservableCollection<BoardSummaryViewModel> Boards { get; set; }
        public bool RequiredLogin { get; set; }
        public bool IsLoading { get; set; }

        protected async override void OnInitialize()
        {
            try
            {
                await _viewReady.Task;
                IsLoading = true;
                if ((await _trelloClient.Initialize()))
                {
                    RequiredLogin = false;
                    var boards = await _trelloClient.GetBoardsForMe();
                    Boards.Clear();
                    Boards.AddRange(
                        boards.Where(x => !x.Closed).OrderBy(x => x.Name).Select(x => new BoardSummaryViewModel(x)));
                }
                else
                    RequiredLogin = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override void OnActivate()
        {
            if(RequiredLogin)
                OnInitialize();
        }

        protected override void OnViewReady(object view)
        {
            if (!_viewReady.Task.IsCompleted) _viewReady.SetResult(true);
        }

        /* TODO: ConnectWithTrello method */



















        public void NavigateToBoard(BoardSummaryViewModel viewModel)
        {
            _navigationService.ForView<BoardViewModel>()
                .WithParam(x => x.BoardId, viewModel.Board.Id)
                .WithParam(x => x.DisplayName, viewModel.Board.Name)
                .Navigate();
        }

        public void NavigateToAbout()
        {
            _navigationService.Navigate<AboutViewModel>();
        }

        public void Handle(Refresh message)
        {
            if (IsActive) OnInitialize();
        }
    }
}