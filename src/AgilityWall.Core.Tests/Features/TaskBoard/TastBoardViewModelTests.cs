using System;
using AgilityWall.Core.Features.TaskBoard;
using AgilityWall.Core.Navigation;
using Caliburn.Micro;
using NSubstitute;
using NUnit.Framework;
using PortableTrello.Client;
using PortableTrello.Client.Requests;
using PortableTrello.Client.Requests.BoardRequests;

namespace AgilityWall.Core.Tests.Features.TaskBoard
{
    [TestFixture]
    public class BoardViewModelTests
    {
        private BoardViewModel _sut;
        private ITrelloClient _trelloClient;

        [SetUp]
        public void GivenATaskBoard()
        {
            _trelloClient = Substitute.For<ITrelloClient>();
            _sut = new BoardViewModel(Substitute.For<INavService>(), _trelloClient, Substitute.For<ListSummaryViewModel.Factory>());
        }

        [Test]
        public void WhenTheBoardIdIsSet_ThenThatBoardIsLoadedFromTheApi()
        {
            _sut.BoardId = "1";

            ((IViewAware)_sut).AttachView(new Object());
            ((IActivate)_sut).Activate();

            _trelloClient.Received().ExecuteRequest(Arg.Is<GetBoardByIdRequest>(x => x.Resource == ResourcePathFor.Board(_sut.BoardId)));
        }

        [Test]
        public void WhenTheBoardIdIsSetViaTheParameterObject_ThenThatBoardIsLoadedFromTheApi()
        {
            _sut.Parameter = new { BoardId = 1 };

            ((IViewAware)_sut).AttachView(new Object());
            ((IActivate)_sut).Activate();

            _trelloClient.Received().ExecuteRequest(Arg.Is<GetBoardByIdRequest>(x => x.Resource == ResourcePathFor.Board("1")));
        }
    }
}