using System.Collections.Generic;
using System.Threading.Tasks;
using AgilityWall.TrelloApi.Contracts;
using AgilityWall.TrelloApi.Internal;

namespace AgilityWall.TrelloApi.Client
{
    public class TrelloClient : BaseClient
    {
        private readonly ITokenStore _tokenStore;
        const byte ApiVersion = 1;

        public TrelloClient(string key, string secret, ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
            Key = key;
            Secret = secret;
            ClientName = ".NET Portable Trello Client";
        }

        public async Task Initialize()
        {
            Token = await _tokenStore.GetToken();
        }

        protected override string BaseUrl
        {
            get { return string.Format("https://api.trello.com/{0}", ApiVersion); }
        }

        public string Key { get; set; }
        public string Secret { get; set; }
        public string ClientName { get; set; }
        protected TrelloToken Token { get; set; }

        public string GetAuthorizeUrl()
        {
            return BuildUri("/authorize",
                new Dictionary<string, string>
                {
                    {"key", Key},
                    {"name", ClientName},
                    {"expiration", "never"},
                    {"response_type", "token"},
                    {"scope", "read,write"}
                });
        }

        public Task<bool> ProcessAuthorizeResponse(string content)
        {
            //set token
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Board>> GetBoardsForMe()
        {
            return await GetBoardsForUser("me");
        }

        public async Task<IEnumerable<Board>> GetBoardsForUser(string userId)
        {
            var response = await ExecuteRequest<IEnumerable<Board>>(string.Format("/members/{0}/boards", userId),
               new Dictionary<string, string>
                {
                    {"key", Key},
                    {"token", ClientName}
                });
            return response;
        }
    }
}