using System.Threading.Tasks;

namespace AgilityWall.TrelloApi.Client
{
    public interface ITokenStore
    {
        Task<TrelloToken> GetToken();
        Task SaveToken(TrelloToken token);
    }
}