using AgilityWall.Core.Features.TaskBoard;
using AgilityWall.Core.Navigation;
using NSubstitute;
using NUnit.Framework;
using PortableTrello.Client;

namespace AgilityWall.Core.Tests.Features.TaskBoard
{
    [TestFixture]
    public class Given_an_taskboard_viewModel
    {
        private BoardViewModel sut;
        private ITrelloClient _trelloClient;

        [SetUp]
        public void When_the_board_id_is_set()
        {
            _trelloClient = Substitute.For<ITrelloClient>();
            sut = new BoardViewModel(Substitute.For<INavService>(), _trelloClient, Substitute.For<ListSummaryViewModel.Factory>());
        }

        [Test]
        public void Then_that_board_is_loaded_from_the_api()
        {
            
        }
    }
}