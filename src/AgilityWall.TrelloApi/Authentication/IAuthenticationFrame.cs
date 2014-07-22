using System;
using System.Threading.Tasks;

namespace PortableTrello.Authentication
{
    public interface IAuthenticationFrame
    {
        Task DisplayUri(Uri authorizationUrl);
        event EventHandler<BrowserEventArgs> BrowserRedirected;
        event EventHandler LoginCanceled;
    }
}