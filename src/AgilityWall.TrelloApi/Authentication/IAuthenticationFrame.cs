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
}