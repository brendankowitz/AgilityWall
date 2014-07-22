using System.Threading.Tasks;

namespace PortableTrello.Client
{
    public interface ITokenStore
    {
        Task<TrelloToken> GetToken();
        Task SaveToken(TrelloToken token);
        Task Clear();
    }
}