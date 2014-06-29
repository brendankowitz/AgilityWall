using AgilityWall.Core.Infrastructure;
using Caliburn.Micro;
using PropertyChanged;

namespace AgilityWall.Core.Features.TaskBoard
{
    [ImplementPropertyChanged]
    public class BoardViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public string BoardId { get; set; }

        public BoardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected override void OnInitialize()
        {
            if (!string.IsNullOrEmpty(BoardId))
            {
                
            }
        }
    }
}