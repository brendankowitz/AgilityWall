using System;
using System.Threading.Tasks;

namespace AgilityWall.TrelloApi.Authentication
{
    public interface IAuthenticationFrame
    {
        Task DisplayUri(Uri authorizationUrl);
        event EventHandler<BrowserEventArgs> BrowserRedirected;
        event EventHandler LoginCanceled;
    }

    public class BrowserEventArgs : EventArgs
    {
        public BrowserEventArgs(string uri, string htmlContent)
        {
            Uri = uri;
            HtmlContent = htmlContent;
        }

        public string Uri { get; set; }
        public string HtmlContent { get; set; }
    }
}