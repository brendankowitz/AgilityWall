using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using PortableTrello.Client;

namespace PortableTrello.Tests.Client.Authentication
{
    [TestFixture]
    public class AuthenticationTetsts
    {
        private PartiallyMockedTrelloClient _client;
        private MockBrowser _browser;

        [SetUp]
        public void GivenATrelloClient()
        {
            _client = Substitute.ForPartsOf<PartiallyMockedTrelloClient>("test", "test", new InMemoryTokenStore());

            _client.WhenForAnyArgs(x => x.ExecuteRequestStub(Arg.Any<string>(), Arg.Any<IDictionary<string, string>>(), Arg.Any<HttpMethod>(),
                Arg.Any<HttpContent>())).DoNotCallBase();

            _browser = new MockBrowser();
        }

        [Test]
        public void WhenBeginingAuthentication_ThenTheAuthFrameIsRedirected()
        {
            _client.Authenticate(_browser);
            _browser.AuthorizationUrl.Should().Be("https://api.trello.com/1/authorize?key=test&name=.NET Portable Trello Client&expiration=never&response_type=token&scope=read,write");
            _browser.OnLoginCanceled();
        }
    }
}