using System.Threading.Tasks;
using AgilityWall.TrelloApi.Client;

namespace AgilityWall.Core.Infrastructure
{
    public class ObjectTokenStore : ITokenStore
    {
        private readonly IObjectStorageService<TrelloToken> _tokenStorageService;

        private string Key
        {
            get { return _tokenStorageService.CreateStorageKey("trelloToken"); }
        }

        public ObjectTokenStore(IObjectStorageService<TrelloToken> tokenStorageService)
        {
            _tokenStorageService = tokenStorageService;
        }

        public Task<TrelloToken> GetToken()
        {
            return _tokenStorageService.LoadAsync(Key);
        }

        public Task SaveToken(TrelloToken token)
        {
            return _tokenStorageService.SaveAsync(Key, token);
        }
    }
}