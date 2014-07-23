using System.Threading.Tasks;
using PortableTrello.Authentication;
using PortableTrello.Client.Requests;

namespace PortableTrello.Client
{
    public interface ITrelloClient
    {
        Task<bool> Initialize();
        Task<bool> Authenticate(IAuthenticationFrame authenticationFrame);
        Task<TResponse> ExecuteRequest<TRequest, TResponse>(ITrelloRequest<TRequest, TResponse> request);
    }
}