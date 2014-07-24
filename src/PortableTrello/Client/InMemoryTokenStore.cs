using System.Threading.Tasks;

namespace PortableTrello.Client
{
    public class InMemoryTokenStore : ITokenStore
    {
        private TrelloToken _token;
        
        public Task<TrelloToken> GetToken()
        {
            return Task.FromResult(_token);
        }

        public Task SaveToken(TrelloToken token)
        {
            _token = token;
            return Task.FromResult(true);
        }

        public Task Clear()
        {
            _token = null;
            return Task.FromResult(true);
        }
    }
}