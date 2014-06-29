using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AgilityWall.TrelloApi.Authentication;
using AgilityWall.TrelloApi.Client.Parameters;
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

        public async Task<bool> Initialize()
        {
            if (Token == null)
                Token = await _tokenStore.GetToken();
            if (Token != null)
                return true;
            return false;
        }

        protected override string BaseUrl
        {
            get { return string.Format("https://api.trello.com/{0}", ApiVersion); }
        }

        public string Key { get; set; }
        public string Secret { get; set; }
        public string ClientName { get; set; }
        protected TrelloToken Token { get; set; }

        protected string GetAuthorizeUrl()
        {
            return BuildUri("/authorize",
                new Dictionary<string, string>
                {
                    {"key", Key},
                    {"name", ClientName},
                    //{"return_url", "http://bing.com"},
                    {"expiration", "never"},
                    {"response_type", "token"},
                    {"scope", "read,write"}
                });
        }

        async Task<bool> ProcessAuthorizeResponse(string content)
        {
            if (string.IsNullOrEmpty(content))
                return false;

            var matches = Regex.Match(content, "<pre>(?<KEY>.*)</pre>", RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups["KEY"].Value;

            if (!string.IsNullOrEmpty(matches))
            {
                Token = new TrelloToken(matches.Trim(), null);
                await _tokenStore.SaveToken(Token);
                return true;
            }
            return false;
        }

        public async Task<bool> Authenticate(IAuthenticationFrame authenticationFrame)
        {
            string content = null;
            var waitForContent = new AutoResetEvent(false);
            EventHandler<BrowserEventArgs> handler = (sender, s) =>
            {
                var approveUri = BuildUri("/token/approve", new Dictionary<string, string>());
                if (s.Uri.StartsWith(approveUri))
                {
                    content = s.HtmlContent;
                    waitForContent.Set();
                }
                else if (s.Uri.EndsWith("trello.com/"))
                    waitForContent.Set();
            };
            EventHandler cancelHandler = (sender, s) => waitForContent.Set();
            try
            {
                authenticationFrame.BrowserRedirected += handler;
                authenticationFrame.LoginCanceled += cancelHandler;
                await authenticationFrame.DisplayUri(new Uri(GetAuthorizeUrl()));
                await Task.Run(async () =>
                {
                    if (!waitForContent.WaitOne(TimeSpan.FromMinutes(5)))
                        throw new Exception("Authentication timed out.");

                    return await ProcessAuthorizeResponse(content);
                });
            }
            finally
            {
                authenticationFrame.BrowserRedirected -= handler;
                authenticationFrame.LoginCanceled -= cancelHandler;
            }
            return false;
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
                    {"token", Token.Token}
                });
            return response;
        }

        public async Task<Board> GetBoardById(string boardId)
        {
            var response = await ExecuteRequest<Board>(string.Format("/boards/{0}", boardId),
               new Dictionary<string, string>
                {
                    {"key", Key},
                    {"token", Token.Token}
                });
            return response;
        }

        public async Task<IEnumerable<Card>> GetBoardCards(string boardId, GetCardOptions options = GetCardOptions.all)
        {
            var response = await ExecuteRequest<IEnumerable<Card>>(string.Format("/boards/{0}/cards/{1}", boardId, options),
               new Dictionary<string, string>
                {
                    {"key", Key},
                    {"token", Token.Token}
                });
            return response;
        }
    }
}