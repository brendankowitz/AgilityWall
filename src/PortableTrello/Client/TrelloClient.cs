using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PortableTrello.Authentication;
using PortableTrello.Client.Requests;
using PortableTrello.Internal;

namespace PortableTrello.Client
{
    public class TrelloClient : BaseClient, ITrelloClient
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
            {
                try
                {
                    Token = await _tokenStore.GetToken();
                }
                catch
                {
                    return false;
                }
            }
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
                    {"expiration", "never"},
                    {"response_type", "token"},
                    {"scope", "read,write"}
                });
        }

        async Task<bool> ProcessAuthorizeResponse(string content)
        {
            if (string.IsNullOrEmpty(content))
                return false;

            var value = Regex.Match(content, "<pre>(?<KEY>.*)</pre>", RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups["KEY"].Value;

            if (!string.IsNullOrEmpty(value))
            {
                Token = new TrelloToken(value.Trim(), null);
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
                if (s.Uri.Contains("/token/approve"))
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

        protected async override Task<string> HandleResponseMessage(HttpResponseMessage message)
        {
            string content = null;
            try
            {
                content = await message.Content.ReadAsStringAsync();
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch { }

            if (!message.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(content) &&
                    content.IndexOf("invalid token", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    Token = null;
                    await _tokenStore.Clear();
                    throw new InvalidTokenException("Your Trello token has expired or is invalid, please login again.");
                }
                throw new HttpRequestException("Request failed.");
            }

            return content;
        }

        public async Task<TResponse> ExecuteRequest<TRequest, TResponse>(ITrelloRequest<TRequest, TResponse> request)
        {
            var response = await ExecuteRequest<TResponse>(request.Resource,
                   new Dictionary<string, string>(request.Parameters)
                    {
                        {"key", Key},
                        {"token", Token.Value}
                    });
            return response;
        }
    }
}