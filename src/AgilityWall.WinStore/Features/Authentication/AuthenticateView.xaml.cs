using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AgilityWall.WinStore.Controls;
using PortableTrello.Authentication;

namespace AgilityWall.WinStore.Features.Authentication
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AuthenticateView : Page, IAuthenticationFrame
    {
        public event EventHandler<BrowserEventArgs> BrowserRedirected;
        public event EventHandler LoginCanceled;

        public AuthenticateView()
        {
            InitializeComponent();
            Browser.NavigationCompleted += BrowserOnNavigationCompleted;
            Browser.NavigationStarting += BrowserOnNavigationStarting;
        }

        private void BrowserOnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            SetProgress(true);
        }

        private void BrowserOnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            var htmlContent = GetHtml();
            OnBrowserRedirected(new BrowserEventArgs(args.Uri.ToString(), htmlContent));
            SetProgress(false);
        }

        private string GetHtml()
        {
            return Browser.InvokeScript("eval", new[] {"document.documentElement.outerHTML;"});
        }

        public async Task DisplayUri(Uri authorizationUrl)
        {
            Browser.Navigate(authorizationUrl);
        }

        void SetProgress(bool enabled)
        {
            PageLayout.PageState = enabled ? PageStates.Busy : PageStates.Normal;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            OnLoginCanceled();
        }


        private void OnBrowserRedirected(BrowserEventArgs e)
        {
            var handler = BrowserRedirected;
            if (handler != null) handler(this, e);
        }

        private void OnLoginCanceled()
        {
            var handler = LoginCanceled;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
