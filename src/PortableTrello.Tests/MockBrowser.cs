using System;
using System.Threading.Tasks;
using PortableTrello.Authentication;

namespace PortableTrello.Tests
{
    public class MockBrowser : IAuthenticationFrame
    {
        public Uri AuthorizationUrl { get; set; }

        public Task DisplayUri(Uri authorizationUrl)
        {
            AuthorizationUrl = authorizationUrl;
            return Task.FromResult(true);
        }

        public event EventHandler<BrowserEventArgs> BrowserRedirected;

        public void OnBrowserRedirected(BrowserEventArgs e)
        {
            var handler = BrowserRedirected;
            if (handler != null) handler(this, e);
        }

        public event EventHandler LoginCanceled;

        public void OnLoginCanceled()
        {
            var handler = LoginCanceled;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}